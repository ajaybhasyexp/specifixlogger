namespace SpecifixLogger
{
    public static class Queries
    {
        public const string CheckTable = "SELECT count(*) FROM information_schema.TABLES WHERE (TABLE_SCHEMA = '{0}') AND (TABLE_NAME = 'Logs')";

        public const string CreateTable = @"CREATE TABLE {0}.Logs (
											Identifier varchar(50) NOT NULL DEFAULT 'this is a key',
											Application varchar(100) NULL,
											TimeStampUtc DATETIME NOT NULL,
											Category varchar(100) NULL,
											`Level` INT NOT NULL,
											`Text` varchar(1000) NULL,
											ExceptionMessage varchar(500) NULL,
											InnerExceptionMessage varchar(500) NULL,
											StackTrace varchar(1000) NULL,
											InnerExceptionStackTrace varchar(1000) NULL,
											EventId INT NULL,
											EventName varchar(300) NULL,
											StateText varchar(500) NULL,
											Environment varchar(200) NULL
										)
										ENGINE=InnoDB
										DEFAULT CHARSET=utf8mb4
										COLLATE=utf8mb4_0900_ai_ci;";

    }
}
