CREATE TABLE [dbo].[DocumentTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Date] [date] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Category] [int] NOT NULL,
	[DocumentId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DocumentTransactions] ADD  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[DocumentTransactions]  WITH CHECK ADD FOREIGN KEY([DocumentId])
REFERENCES [dbo].[FinanceDocuments] ([Id])
ON DELETE CASCADE
GO
