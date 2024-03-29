CREATE TABLE [dbo].[Companies](
	[Vat] [nvarchar](20) NOT NULL,
	[DocumentId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Vat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Companies]  WITH CHECK ADD FOREIGN KEY([DocumentId])
REFERENCES [dbo].[FinanceDocuments] ([Id])
GO
