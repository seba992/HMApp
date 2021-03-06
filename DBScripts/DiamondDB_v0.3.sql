USE [master]
GO
/****** Object:  Database [DiamondDB]    Script Date: 2015-12-20 16:26:44 ******/
CREATE DATABASE [DiamondDB]

ALTER DATABASE [DiamondDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DiamondDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DiamondDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DiamondDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DiamondDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DiamondDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DiamondDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DiamondDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DiamondDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DiamondDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DiamondDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DiamondDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DiamondDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DiamondDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DiamondDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DiamondDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DiamondDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DiamondDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DiamondDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DiamondDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DiamondDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DiamondDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DiamondDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DiamondDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DiamondDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DiamondDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DiamondDB] SET  MULTI_USER 
GO
ALTER DATABASE [DiamondDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DiamondDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DiamondDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DiamondDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [DiamondDB]
GO
/****** Object:  Table [dbo].[AccountPrivileges]    Script Date: 2015-12-20 16:26:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountPrivileges](
	[Id] [int] NOT NULL,
	[AccountType] [varchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropAccomodation]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropAccomodation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_proposition] [int] NULL,
	[TypeOfRoom] [varchar](30) NULL,
	[BruttoPrice] [real] NULL,
	[Vat] [real] NULL,
	[Amount] [int] NULL,
	[Days] [int] NULL,
 CONSTRAINT [PK__PropAcco__3214EC074432853A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropAccomodation_Dictionary]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropAccomodation_Dictionary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeOfRoom] [varchar](60) NOT NULL,
	[Price] [real] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropAccomodationDiscount]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropAccomodationDiscount](
	[Id_proposition] [int] NOT NULL,
	[StandardPrice] [real] NULL,
	[Discount] [real] NULL,
	[DoubleRoomEP] [real] NULL,
	[BussinesSingleEP] [real] NULL,
	[ApartmentSingleEP] [real] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_proposition] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropClient]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropClient](
	[Id_proposition] [int] NOT NULL,
	[CompanyName] [varchar](50) NULL,
	[CompanyAdress] [varchar](200) NULL,
	[NIP] [varchar](10) NULL,
	[CustomerFullName] [varchar](40) NULL,
	[PhoneNum] [varchar](15) NULL,
	[DecisingPersonFullName] [varchar](20) NULL,
	[CustomerEmail] [varchar](50) NULL,
 CONSTRAINT [PK__PropClie__EDA6D0E6782D13AF] PRIMARY KEY CLUSTERED 
(
	[Id_proposition] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropExtraServices]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropExtraServices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_proposition] [int] NULL,
	[ServiceType] [varchar](60) NULL,
	[BruttoPrice] [real] NULL,
	[Vat] [real] NULL,
	[Amount] [int] NULL,
	[Days] [int] NULL,
 CONSTRAINT [PK__PropExtr__3214EC0766489883] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropExtraServices_Dictionary]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropExtraServices_Dictionary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceType] [varchar](60) NOT NULL,
	[Price] [real] NULL,
 CONSTRAINT [PK__PropExtr__3214EC07A80FBB81] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropExtraServicesDiscount]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropExtraServicesDiscount](
	[Id_proposition] [int] NOT NULL,
	[StandardPrice] [real] NULL,
	[Discount] [real] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_proposition] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropHallEquipment]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[PropHallEquipment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_proposition] [int] NULL,
	[Things] [varchar](50) NULL,
	[BruttoPrice] [real] NULL,
	[Vat] [real] NULL,
	[Amount] [int] NULL,
	[Days] [int] NULL,
 CONSTRAINT [PK_PropHallEquipment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropHallEquipmentDiscount]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropHallEquipmentDiscount](
	[Id_proposition] [int] NOT NULL,
	[StandardPrice] [real] NULL,
	[Discount] [real] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_proposition] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropHallEquipmnet_Dictionary_First]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropHallEquipmnet_Dictionary_First](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Things] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropHallEquipmnet_Dictionary_Second]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[PropHallEquipmnet_Dictionary_Second](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Things] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropMenuGastronomicThings_Dictionary_First]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropMenuGastronomicThings_Dictionary_First](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ThingName] [varchar](170) NULL,
	[NettoMini] [real] NULL,
	[Vat] [real] NULL,
	[MergeType] [varchar](5) NULL,
	[SpecificType] [varchar](10) NULL,
 CONSTRAINT [PK__PropMenu__3214EC07DF4ABE8E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropMenuMerge]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropMenuMerge](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_proposition] [int] NOT NULL,
	[MergeName] [varchar](40) NULL,
	[DefaultValue] [real] NULL,
	[MergeType] [varchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropMenuMerge_Dictionary_First]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[PropMenuMerge_Dictionary_First](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MergeName] [varchar](40) NULL,
	[Value] [real] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropMenuPosition]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropMenuPosition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_proposition] [int] NULL,
	[TypeOfService] [varchar](170) NULL,
	[BruttoPrice] [real] NULL,
	[Vat] [real] NULL,
	[Amount] [int] NULL,
	[Days] [int] NULL,
	[MergeType] [varchar](5) NULL,
 CONSTRAINT [PK__PropMenu__3214EC07B15CECE7] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropMergeTypes_Dictionary]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropMergeTypes_Dictionary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MergeType] [varchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proposition]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proposition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_user] [int] NOT NULL,
	[UpdateDate] [date] NOT NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK__Proposit__3214EC07B4C8EBAF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropositionStates_Dictionary]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropositionStates_Dictionary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropPaymentSuggestions]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropPaymentSuggestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_proposition] [int] NOT NULL,
	[PaymentForm] [varchar](60) NULL,
	[InvoiceServiceName] [varchar](255) NULL,
	[IndividualOrders] [varchar](170) NULL,
	[CarPark] [varchar](90) NULL,
	[HallDescription] [varchar](max) NULL,
 CONSTRAINT [PK__PropPaym__3214EC07DDC91D22] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropPaymentSuggestions_Dictionary_First]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropPaymentSuggestions_Dictionary_First](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentForm] [varchar](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropPaymentSuggestions_Dictionary_Fourth]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropPaymentSuggestions_Dictionary_Fourth](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarPark] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropPaymentSuggestions_Dictionary_Second]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropPaymentSuggestions_Dictionary_Second](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceServiceName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropPaymentSuggestions_Dictionary_Third]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropPaymentSuggestions_Dictionary_Third](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IndividualOrders] [varchar](170) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropReservationDetails]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[PropReservationDetails](
	[Id_proposition] [int] NOT NULL,
	[StartData] [date] NULL,
	[StartTime] [time](7) NULL,
	[EndData] [date] NULL,
	[EndTime] [time](7) NULL,
	[PeopleNumber] [int] NULL,
	[Hall] [varchar](50) NULL,
	[HallSetting] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_proposition] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropReservationDetails_Dictionary_HallCapacity]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropReservationDetails_Dictionary_HallCapacity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Hall] [varchar](20) NULL,
	[Area] [int] NULL,
	[TheatrePeopleNumber] [int] NULL,
	[UShapePeopleNumber] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropReservationDetails_Dictionary_HallPrices]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropReservationDetails_Dictionary_HallPrices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Hall] [varchar](20) NULL,
	[January] [int] NULL,
	[February] [int] NULL,
	[March] [int] NULL,
	[April] [int] NULL,
	[May] [int] NULL,
	[June] [int] NULL,
	[July] [int] NULL,
	[August] [int] NULL,
	[September] [int] NULL,
	[October] [int] NULL,
	[November] [int] NULL,
	[December] [int] NULL,
	[Other] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropReservationDetails_Dictionary_HallSettings]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropReservationDetails_Dictionary_HallSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Setting] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](35) NOT NULL,
	[Surname] [varchar](35) NOT NULL,
	[PhoneNum] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
	[Position] [varchar](70) NULL,
	[AccountType] [int] NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](256) NOT NULL,
	[FirstLogin] [varchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VatList]    Script Date: 2015-12-20 16:26:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VatList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Vat] [real] NULL,
 CONSTRAINT [PK__VatList__3214EC07C74C614B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PropAccomodation]  WITH CHECK ADD  CONSTRAINT [FK__PropAccom__Id_pr__3A81B327] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropAccomodation] CHECK CONSTRAINT [FK__PropAccom__Id_pr__3A81B327]
GO
ALTER TABLE [dbo].[PropAccomodationDiscount]  WITH CHECK ADD  CONSTRAINT [FK__PropAccom__Id_pr__4316F928] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropAccomodationDiscount] CHECK CONSTRAINT [FK__PropAccom__Id_pr__4316F928]
GO
ALTER TABLE [dbo].[PropClient]  WITH CHECK ADD  CONSTRAINT [FK__PropClien__Id_pr__440B1D61] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropClient] CHECK CONSTRAINT [FK__PropClien__Id_pr__440B1D61]
GO
ALTER TABLE [dbo].[PropExtraServices]  WITH CHECK ADD  CONSTRAINT [FK__PropExtra__Id_pr__3D5E1FD2] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropExtraServices] CHECK CONSTRAINT [FK__PropExtra__Id_pr__3D5E1FD2]
GO
ALTER TABLE [dbo].[PropExtraServicesDiscount]  WITH CHECK ADD  CONSTRAINT [FK__PropExtra__Id_pr__45F365D3] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropExtraServicesDiscount] CHECK CONSTRAINT [FK__PropExtra__Id_pr__45F365D3]
GO
ALTER TABLE [dbo].[PropHallEquipment]  WITH CHECK ADD  CONSTRAINT [FK_PropHallEquipment_Proposition] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
GO
ALTER TABLE [dbo].[PropHallEquipment] CHECK CONSTRAINT [FK_PropHallEquipment_Proposition]
GO
ALTER TABLE [dbo].[PropHallEquipmentDiscount]  WITH CHECK ADD  CONSTRAINT [FK__PropHallE__Id_pr__47DBAE45] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropHallEquipmentDiscount] CHECK CONSTRAINT [FK__PropHallE__Id_pr__47DBAE45]
GO
ALTER TABLE [dbo].[PropMenuMerge]  WITH CHECK ADD  CONSTRAINT [FK__PropMenuM__Id_pr__48CFD27E] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropMenuMerge] CHECK CONSTRAINT [FK__PropMenuM__Id_pr__48CFD27E]
GO
ALTER TABLE [dbo].[PropMenuPosition]  WITH CHECK ADD  CONSTRAINT [FK__PropMenuP__Id_pr__31EC6D26] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropMenuPosition] CHECK CONSTRAINT [FK__PropMenuP__Id_pr__31EC6D26]
GO
ALTER TABLE [dbo].[Proposition]  WITH CHECK ADD  CONSTRAINT [FK__Propositi__Id_us__1FCDBCEB] FOREIGN KEY([Id_user])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Proposition] CHECK CONSTRAINT [FK__Propositi__Id_us__1FCDBCEB]
GO
ALTER TABLE [dbo].[PropPaymentSuggestions]  WITH CHECK ADD  CONSTRAINT [FK__PropPayme__Id_pr__403A8C7D] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropPaymentSuggestions] CHECK CONSTRAINT [FK__PropPayme__Id_pr__403A8C7D]
GO
ALTER TABLE [dbo].[PropReservationDetails]  WITH CHECK ADD  CONSTRAINT [FK__PropReser__Id_pr__4CA06362] FOREIGN KEY([Id_proposition])
REFERENCES [dbo].[Proposition] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropReservationDetails] CHECK CONSTRAINT [FK__PropReser__Id_pr__4CA06362]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([AccountType])
REFERENCES [dbo].[AccountPrivileges] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [DiamondDB] SET  READ_WRITE 
GO
