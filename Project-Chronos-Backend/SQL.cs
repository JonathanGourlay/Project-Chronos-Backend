namespace Project_Chronos_Backend
{
    public class Sql
    {
        public const string GetItems = @"
        SELECT TOP (1000)
	        [ItemID]
              ,[Name]
              ,[Description]
              ,[Price]
              ,[StockCount]
              ,[ProductImage]
          FROM [dbo].[Items_Table]
        ";

        public const string GetItemById = @"
        SELECT TOP (1000)
	        [ItemID]
              ,[Name]
              ,[Description]
              ,[Price]
              ,[StockCount]
              ,[ProductImage]
        FROM [dbo].[Items_Table]
        WHERE ItemId = @Id    
        ";

        public const string Create = @"
        INSERT INTO [dbo].[Items_Table]
           ([ItemID]
           ,[Name]
           ,[Description]
           ,[Price]
           ,[StockCount]
           ,[ProductImage])
     VALUES
            (@Name,
             @Price,
             @Description,
             @StockCount,
             @ProductImage)
         ";
    }
}
