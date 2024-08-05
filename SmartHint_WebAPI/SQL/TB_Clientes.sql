USE [smarthint]
GO

/****** Object:  Table [dbo].[Clientes]    Script Date: 05/08/2024 17:49:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Clientes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](150) NOT NULL,
	[email] [varchar](150) NULL,
	[telefone] [int] NULL,
	[tipo] [varchar](150) NULL,
	[CPF] [int] NULL,
	[CNPJ] [int] NULL,
	[inscricaoEstadual] [varchar](150) NULL,
	[bloqueado] [bit] NULL,
	[genero] [char](1) NULL,
	[dataNascimento] [date] NULL,
	[senha] [varchar](255) NULL,
	[dataCadastro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

