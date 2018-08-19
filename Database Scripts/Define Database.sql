--create database

If Not Exists(Select 1 From Sys.Databases Where Name=N'PatientPortal')
Begin
		Create Database [PatientPortal]
End;

Use PatientPortal;
go

If Not Exists (Select 1 From Sysobjects Where Name = 'TB_Patient' and Xtype = 'U')
Begin

		CREATE TABLE [dbo].[TB_Patient](
			[Patient_ID] [int] IDENTITY(1,1) NOT NULL,	
			[First_Name] [varchar](100) Not NULL,
			[Last_Name] [varchar](100) Not NULL,
			[DateOfBirth] DateTime Not NULL,
			[PhoneNumber] [varchar](25) NOT NULL,	
			[IsActive] [bit] NOT NULL,
			[CreateDate] [datetime] NOT NULL,
			[CreatedBy] [varchar](200) NOT NULL,
			[UpdateDate] [datetime] NULL,
			[UpdatedBy] [varchar](200) NULL,
		 CONSTRAINT [PK__TB_Patient] PRIMARY KEY CLUSTERED 
		(
			[Patient_ID] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		 CONSTRAINT [UC_User] UNIQUE NONCLUSTERED 
		(
			[First_Name] ASC,
			[Last_Name] ASC,
			[DateOfBirth] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
End;
GO
If Not Exists (Select 1 From Sysobjects Where Name = 'TB_RxData' and Xtype = 'U')
Begin
		CREATE TABLE [dbo].[TB_RxData](
			[RxData_ID] [int] IDENTITY(1,1) NOT NULL,				
			[RxDate] DateTime Not NULL,
			[RxDoctor] [varchar](200) NOT NULL,	
			[Prescription1] [varchar](1000) NOT NULL,	
			[Prescription2] [varchar](1000) NULL,	
			[Prescription3] [varchar](1000) NULL,	
			[Prescription4] [varchar](1000) NULL,	
			[Prescription5] [varchar](1000) NULL,	
			[CreateDate] [datetime] NOT NULL,
			[CreatedBy] [varchar](200) NOT NULL,
			[UpdateDate] [datetime] NULL,
			[UpdatedBy] [varchar](200) NULL,
		 CONSTRAINT [PK__TB_RxData] PRIMARY KEY CLUSTERED 
		(
			[RxData_ID] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
End;
go
If Not Exists (Select 1 From Sysobjects Where Name = 'TB_PatientRxData' and Xtype = 'U')
Begin
		CREATE TABLE [dbo].[TB_PatientRxData](
			[PatientRxData_ID] [int] IDENTITY(1,1) NOT NULL,				
			[Patient_ID] [int] NOT NULL,				
			[RxData_ID] [int] NOT NULL,							
			[CreateDate] [datetime] NOT NULL,
			[CreatedBy] [varchar](200) NOT NULL,
			[UpdateDate] [datetime] NULL,
			[UpdatedBy] [varchar](200) NULL,
		 CONSTRAINT [PK__TB_PatientRxData] PRIMARY KEY CLUSTERED 
		(
			[PatientRxData_ID] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
End;
go
If Not Exists (Select 1 From Sysobjects Where Name = 'TB_PortalUser' and Xtype = 'U')
Begin
		CREATE TABLE [dbo].[TB_PortalUser](
			[PortalUser_ID] [int] IDENTITY(1,1) NOT NULL,				
			[UserName] [varchar](200) NOT NULL,
			[DisplayName] [varchar](200) NOT NULL,
			[HashedPassword] [varchar](1000) NOT NULL,
			[IsActive] [bit] NOT NULL,					
			[CreateDate] [datetime] NOT NULL,
			[CreatedBy] [varchar](200) NOT NULL,
			[UpdateDate] [datetime] NULL,
			[UpdatedBy] [varchar](200) NULL,
		 CONSTRAINT [PK__TB_PortalUser] PRIMARY KEY CLUSTERED 
		(
			[PortalUser_ID] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
End;

go
If Not Exists (Select * From Sys.Objects o WHERE o.Object_id = Object_id(N'[dbo].[FK_Patient_PatientRxData]') And OBJECTPROPERTY(o.object_id, N'IsForeignKey') = 1)
Begin
		ALTER TABLE [dbo].[TB_PatientRxData]  WITH CHECK ADD  CONSTRAINT [FK_Patient_PatientRxData] FOREIGN KEY([Patient_ID])
		REFERENCES [dbo].[TB_Patient] ([Patient_ID]);
End;
GO
If Not Exists (Select * From Sys.Objects o WHERE o.Object_id = Object_id(N'[dbo].[FK_Patient_RxData]') And OBJECTPROPERTY(o.object_id, N'IsForeignKey') = 1)
Begin
		ALTER TABLE [dbo].[TB_PatientRxData]  WITH CHECK ADD  CONSTRAINT [FK_Patient_RxData] FOREIGN KEY([RxData_ID])
		REFERENCES [dbo].[TB_RxData] ([RxData_ID]);
End;
GO
if not exists(Select 1 From tb_PortalUser where UserName='Admin')
	Insert into tb_PortalUser(UserName,DisplayName,HashedPassword,IsActive,CreatedBy,CreateDate)
	Select 'Admin','Hospital Administrator',Convert(Varchar(32), HashBytes('MD5', 'password'), 2),1, 'System',getdate();

GO

--select CONVERT(VARCHAR(32), HashBytes('MD5', 'email@dot.com'), 2)
----drop table [TB_PatientRx]
--select len('F53BD08920E5D25809DF2563EF9C52B6');
Select * from TB_PortalUser;