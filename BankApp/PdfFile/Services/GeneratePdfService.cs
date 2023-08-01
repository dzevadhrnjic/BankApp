using BankApp.Accounts.Data;
using BankApp.Accounts.Exceptions;
using BankApp.Accounts.Services;
using BankApp.EmailVerification.Exceptions;
using BankApp.PdfFile.Data;
using BankApp.PdfFile.Models;
using BankApp.Users.Services;
using BankApp.Users.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BankApp.PdfFile.Services
{
    public class GeneratePdfService : IGeneratePdfService
    {
        private readonly AccountDbContext _accountDbContext;
        private readonly IUserService _userService;
        private readonly TokenUtil _tokenUtil;
        private readonly IEmailService _emailService;
        private readonly StatementDbContext _statementDbContext;

        public GeneratePdfService(AccountDbContext accountDbContext,
            IAccountService accountService,
            IUserService userService,
            TokenUtil token,
            IEmailService emailService,
            StatementDbContext statementDbContext)
        {
            _accountDbContext = accountDbContext;
            _userService = userService; 
            _tokenUtil = token;
            _emailService = emailService;
            _statementDbContext = statementDbContext;
        }

        public List<Statement> Statement(string token, int accountId)
        {
            var userId = _tokenUtil.VerifyToken(token);
            var idAccount = _accountDbContext.Accounts.FirstOrDefault(x => x.Id == accountId && x.UserId == userId);
            var user = _userService.GetById(userId);

            if (idAccount == null)
            {
                throw new AccountNotFound("No account with that id");
            }

            if (!user.VerifyEmail)
            {
                throw new EmailVerificationException("Please verify email");
            }

            CreatePdfFile(accountId);

            _emailService.SendEmailWithAttachment(user.Email, "Statement", "Your statement " + user.FirstName);

            return null;
        }

        public void CreatePdfFile(int accountId)
        {
            Document document = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("transactions.pdf", FileMode.Create));

            Paragraph paragraph = new Paragraph();

            document.Open();
        
            var headingFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
            var headingParagraph = new Paragraph("Statement", headingFont);
            headingParagraph.Alignment = Element.ALIGN_CENTER;

            var columnText = new ColumnText(writer.DirectContent);
            columnText.SetSimpleColumn(new Rectangle(36, 806, 559, 836));
            columnText.AddElement(headingParagraph);
            columnText.Go();

            var table = new PdfPTable(5);

            table.SetWidthPercentage(new float[] { 110f, 110f, 110f, 110f, 110f }, PageSize.LETTER);

            var headerFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);

            table.AddCell(new PdfPCell(new Phrase("Sender Name", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Receiver Name", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Amount", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Created At", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Email", headerFont)));

            var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

            foreach (var statement in GetTransactionsForStatement(accountId))
            {
                table.AddCell(new PdfPCell(new Phrase(statement.SenderName, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(statement.ReceiverName, cellFont)));
                table.AddCell(new PdfPCell(new Phrase(statement.Amount.ToString(), cellFont)));
                table.AddCell(new PdfPCell(new Phrase(statement.CreatedAt.ToString(), cellFont)));
                table.AddCell(new PdfPCell(new Phrase(statement.Email, cellFont)));
            }

            document.Add(table);
            document.Close();
            writer.Close();
        }

        public List<Statement> GetTransactionsForStatement(int accountId)
        {
            var query = (
                from t in _statementDbContext.Transactions
                where t.SourceAccount == accountId
                join sa in _statementDbContext.Accounts on t.SourceAccount equals sa.Id
                join su in _statementDbContext.Users on sa.UserId equals su.Id
                join da in _statementDbContext.Accounts on t.DestinationAccount equals da.Id
                join du in _statementDbContext.Users on da.UserId equals du.Id
                where t.Id == t.Id
                select new Statement
                {
                    SenderName = su.FirstName + " " + su.LastName,
                    ReceiverName = du.FirstName + " " + du.LastName,
                    Amount = t.Amount,
                    CreatedAt = t.CreatedAt,
                    Email = su.Email
                }).ToList();
    
            return query;
        }
    }
}
