using Dapper;
using Domain.DTOs.Request;
using Domain.Models;
using Infrastructure.Persistence.Mappers.Entities;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Infrastructure.Persistence.Repositories
{
    public class InvoiceRepositoryImpl : InvoiceRepository
    {
        private Settings _settings;

        public InvoiceRepositoryImpl(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        protected MySqlConnection DbConnection()
        {
            return new MySqlConnection(_settings.ServiceConfig.ConnectionString);
        }

        public async Task<IEnumerable<InvoiceEntity>> GetAllInvoices()
        {
            var db = DbConnection();

            var sql = @"SELECT id, client, date, subtotal, discount, total 
                        FROM invoice";

            return await db.QueryAsync<InvoiceEntity>(sql);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProductsByInvoiceId(int id)
        {
            var db = DbConnection();

            var sql = @"SELECT id, product_id, quantity, description, invoice_id 
                        FROM product
                        WHERE invoice_id = @Id";

            return await db.QueryAsync<ProductEntity>(sql, new { Id = id });
        }

        public async Task<bool> SaveInvoice(InvoiceDto invoice)
        {
            var db = DbConnection();

            var sql = @"INSERT INTO invoice(client, date, subtotal, discount, total) 
                        Values(@Client, @Date, @Subtotal, @Discount, @Total)";

            var result = await db.ExecuteAsync(sql,
                new { invoice.Client, invoice.Date, invoice.Subtotal, invoice.Discount, invoice.Total });

            return result > 0;
        }

        public async Task<bool> UpdateInvoice(InvoiceDto invoice)
        {
            var db = DbConnection();

            var sql = @"Update invoice
                        SET id = @Id, 
                            client = @Client, 
                            date = @Date, 
                            subtotal = @Subtotal, 
                            discount = @Discount, 
                            total = @Total
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(sql,
                new { invoice.Id, invoice.Client, invoice.Date, invoice.Subtotal, invoice.Discount, invoice.Total });

            return result > 0;
        }
    }
}
