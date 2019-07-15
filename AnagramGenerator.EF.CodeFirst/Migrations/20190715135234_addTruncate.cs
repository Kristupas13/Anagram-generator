using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramGenerator.EF.CodeFirst.Migrations
{
    public partial class addTruncate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"USE [Solver_DB]
GO
/****** Object:  StoredProcedure [dbo].[TruncateTable]    Script Date: 7/15/2019 4:44:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kristupas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[TruncateTable] 
	-- Add the parameters for the stored procedure here
	@TableName nvarchar(255) = 0, 
	@p2 int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    -- Insert statements for procedure here
	DECLARE @sql nvarchar(255)
	SET @sql = 'TRUNCATE TABLE ' + @TableName
	EXEC(@sql)


END 
";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
