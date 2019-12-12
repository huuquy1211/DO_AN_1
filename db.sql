USE [QLDia]
GO
/****** Object:  Table [dbo].[ChiTietPhieuThue]    Script Date: 12/12/2019 9:54:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuThue](
	[maCTPhieuThue] [int] IDENTITY(1,1) NOT NULL,
	[maDia] [int] NOT NULL,
	[maPhieuThue] [int] NOT NULL,
	[hanTra] [date] NULL,
	[trangThaiTra] [bit] NULL,
 CONSTRAINT [PK__ChiTietP__538C0CAD06A3A808] PRIMARY KEY CLUSTERED 
(
	[maCTPhieuThue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dia]    Script Date: 12/12/2019 9:54:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dia](
	[maDia] [int] IDENTITY(1,1) NOT NULL,
	[maTuaDe] [int] NULL,
	[loai] [varchar](50) NULL,
	[trangThai] [int] NULL,
 CONSTRAINT [PK__Dia__24315F202E3E3068] PRIMARY KEY CLUSTERED 
(
	[maDia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 12/12/2019 9:54:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KhachHang](
	[maKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[hoTen] [nvarchar](50) NULL,
	[soCMND] [varchar](30) NULL,
	[diaChi] [nvarchar](50) NULL,
	[soDienThoai] [varchar](12) NULL,
	[trangThaiXoa] [bit] NULL,
 CONSTRAINT [PK__KhachHan__0CCB3D49F423CFF9] PRIMARY KEY CLUSTERED 
(
	[maKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 12/12/2019 9:54:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NhanVien](
	[maNhanVien] [int] IDENTITY(1,1) NOT NULL,
	[maTaiKhoan] [int] NOT NULL,
	[hoTen] [nvarchar](20) NULL,
	[diaChi] [nvarchar](50) NULL,
	[soDienThoai] [varchar](12) NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[maNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PhieuThue]    Script Date: 12/12/2019 9:54:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuThue](
	[maPhieuThue] [int] IDENTITY(1,1) NOT NULL,
	[maKhachHang] [int] NOT NULL,
	[tienCoc] [float] NULL,
	[ngayThue] [date] NULL,
	[trangThaiTraPhiTre] [bit] NULL,
 CONSTRAINT [PK__PhieuThu__4C2D27FC7DEDA032] PRIMARY KEY CLUSTERED 
(
	[maPhieuThue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuTra]    Script Date: 12/12/2019 9:54:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuTra](
	[maPhieuTra] [int] IDENTITY(1,1) NOT NULL,
	[ngayTra] [date] NULL,
	[maCTPhieuThue] [int] NOT NULL,
 CONSTRAINT [PK__PhieuTra__C868CC0D792F840D] PRIMARY KEY CLUSTERED 
(
	[maPhieuTra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhiTre]    Script Date: 12/12/2019 9:54:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhiTre](
	[maPhiTre] [int] IDENTITY(1,1) NOT NULL,
	[maPhieuTra] [int] NOT NULL,
	[tongTien] [float] NULL,
	[tinhTrangThanhToan] [bit] NULL,
 CONSTRAINT [PK__PhiTre__39621CFC0988537C] PRIMARY KEY CLUSTERED 
(
	[maPhiTre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 12/12/2019 9:54:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[maTaiKhoan] [int] IDENTITY(1,1) NOT NULL,
	[tenDangNhap] [varchar](30) NOT NULL,
	[matKhau] [varchar](30) NULL,
	[quyenTruyCap] [bit] NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[maTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TuaDe]    Script Date: 12/12/2019 9:54:05 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TuaDe](
	[maTuaDe] [int] IDENTITY(1,1) NOT NULL,
	[tenTuaDe] [nvarchar](50) NULL,
	[soLuong] [int] NULL,
	[donGia] [float] NULL,
	[moTa] [nvarchar](50) NULL,
	[trangThai] [bit] NULL,
	[ngayChoThue] [int] NULL,
 CONSTRAINT [PK__TuaDe__03E51FECB5479386] PRIMARY KEY CLUSTERED 
(
	[maTuaDe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ChiTietPhieuThue] ON 

INSERT [dbo].[ChiTietPhieuThue] ([maCTPhieuThue], [maDia], [maPhieuThue], [hanTra], [trangThaiTra]) VALUES (5112, 1012, 5085, CAST(N'2019-11-20' AS Date), 1)
INSERT [dbo].[ChiTietPhieuThue] ([maCTPhieuThue], [maDia], [maPhieuThue], [hanTra], [trangThaiTra]) VALUES (5113, 1061, 5086, CAST(N'2019-11-20' AS Date), 1)
INSERT [dbo].[ChiTietPhieuThue] ([maCTPhieuThue], [maDia], [maPhieuThue], [hanTra], [trangThaiTra]) VALUES (5114, 1060, 5086, CAST(N'2019-11-20' AS Date), 1)
INSERT [dbo].[ChiTietPhieuThue] ([maCTPhieuThue], [maDia], [maPhieuThue], [hanTra], [trangThaiTra]) VALUES (5115, 1063, 5087, CAST(N'2019-12-16' AS Date), 1)
INSERT [dbo].[ChiTietPhieuThue] ([maCTPhieuThue], [maDia], [maPhieuThue], [hanTra], [trangThaiTra]) VALUES (5116, 1063, 5088, CAST(N'2019-12-16' AS Date), 1)
INSERT [dbo].[ChiTietPhieuThue] ([maCTPhieuThue], [maDia], [maPhieuThue], [hanTra], [trangThaiTra]) VALUES (5117, 1063, 5089, CAST(N'2019-12-16' AS Date), 1)
INSERT [dbo].[ChiTietPhieuThue] ([maCTPhieuThue], [maDia], [maPhieuThue], [hanTra], [trangThaiTra]) VALUES (5118, 1063, 5090, CAST(N'2019-12-16' AS Date), 1)
INSERT [dbo].[ChiTietPhieuThue] ([maCTPhieuThue], [maDia], [maPhieuThue], [hanTra], [trangThaiTra]) VALUES (5119, 1013, 5091, CAST(N'2019-12-16' AS Date), 1)
SET IDENTITY_INSERT [dbo].[ChiTietPhieuThue] OFF
SET IDENTITY_INSERT [dbo].[Dia] ON 

INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1012, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1013, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1014, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1015, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1016, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1017, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1018, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1019, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1020, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1021, 1014, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1022, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1023, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1024, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1025, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1026, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1027, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1028, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1029, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1030, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1031, 1025, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1032, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1033, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1034, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1035, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1036, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1037, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1038, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1039, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1040, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1041, 1018, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1042, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1043, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1044, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1045, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1046, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1047, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1048, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1049, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1050, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1051, 1017, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1052, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1053, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1054, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1055, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1056, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1057, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1058, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1059, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1060, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1061, 1015, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1062, 1017, N'DVD', 3)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1063, 1020, N'DVD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1064, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1065, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1066, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1067, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1068, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1069, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1070, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1071, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1072, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1073, 1020, N'CD', 0)
INSERT [dbo].[Dia] ([maDia], [maTuaDe], [loai], [trangThai]) VALUES (1074, 1020, N'CD', 0)
SET IDENTITY_INSERT [dbo].[Dia] OFF
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1006, N'Phạm Trung Nghĩa', N'1234562457', N'HCM', N'0934123123', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1007, N'Bùi Minh Phụng', N'1234569089', N'Long An', N'0123456777', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1009, N'Hoàng Hữu Cương', N'1234569077', N'Long An', N'0123456786', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1010, N'Trần Bảo Khanh', N'1234569863', N'Long An', N'0123456711', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1011, N'Phạm Minh Đức', N'1234569477', N'Long An', N'0123456223', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1012, N'Lê Bảo Bình', N'1234567895', N'hcm', N'0933323658', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1013, N'Linh Phạm', N'1234567996', N'hcm', N'0933323565', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1014, N'Nguyễn Đình Thuận', N'1234561112', N'hcm', N'0933323636', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1015, N'Nguyễn Đình Phi Hùng', N'1234567775', N'hcm', N'0933327775', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1016, N'Trần Quang Phúc', N'1234563569', N'hcm', N'0933323552', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1023, N'Hoàng Hữu Cương', N'1234563998', N'hcm', N'0933323778', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1024, N'Trần Hồng Lê', N'1234563789', N'hcm', N'0933323113', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1026, N'Phan Hữu Quý', N'1234563569', N'hcm', N'0933323112', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1027, N'Ngô Thái Sang', N'1234563111', N'hcm', N'0933323113', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1028, N'Phạm Minh Đức', N'1234563112', N'hcm', N'0933323114', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1029, N'Trần Sơn Tùng', N'1234563113', N'hcm', N'0933323115', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1030, N'Nguyễn Văn Hậu', N'1234563114', N'hcm', N'0933323116', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1031, N'Nguyễn Hữu Sang', N'1234563115', N'hcm', N'0933323117', 0)
INSERT [dbo].[KhachHang] ([maKhachHang], [hoTen], [soCMND], [diaChi], [soDienThoai], [trangThaiXoa]) VALUES (1032, N'Nguyễn Thị Kim Ngân', N'1234563116', N'hcm', N'0933323118', 0)
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
SET IDENTITY_INSERT [dbo].[NhanVien] ON 

INSERT [dbo].[NhanVien] ([maNhanVien], [maTaiKhoan], [hoTen], [diaChi], [soDienThoai]) VALUES (1004, 1007, N'Phan Huu Quy', N'hcm', N'0933323622')
INSERT [dbo].[NhanVien] ([maNhanVien], [maTaiKhoan], [hoTen], [diaChi], [soDienThoai]) VALUES (1005, 1008, N'Pham Minh Duc', N'hcm', N'0933323621')
INSERT [dbo].[NhanVien] ([maNhanVien], [maTaiKhoan], [hoTen], [diaChi], [soDienThoai]) VALUES (1006, 1009, N'Phan Hữu Quý', N'HCM', N'0933323566')
INSERT [dbo].[NhanVien] ([maNhanVien], [maTaiKhoan], [hoTen], [diaChi], [soDienThoai]) VALUES (1007, 1010, N'Phạm Minh Đức', N'HCM', N'01667006543')
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
SET IDENTITY_INSERT [dbo].[PhieuThue] ON 

INSERT [dbo].[PhieuThue] ([maPhieuThue], [maKhachHang], [tienCoc], [ngayThue], [trangThaiTraPhiTre]) VALUES (5085, 1006, 100000, CAST(N'2019-11-23' AS Date), 1)
INSERT [dbo].[PhieuThue] ([maPhieuThue], [maKhachHang], [tienCoc], [ngayThue], [trangThaiTraPhiTre]) VALUES (5086, 1006, 100000, CAST(N'2019-11-23' AS Date), 1)
INSERT [dbo].[PhieuThue] ([maPhieuThue], [maKhachHang], [tienCoc], [ngayThue], [trangThaiTraPhiTre]) VALUES (5087, 1007, 50000, CAST(N'2019-12-11' AS Date), 1)
INSERT [dbo].[PhieuThue] ([maPhieuThue], [maKhachHang], [tienCoc], [ngayThue], [trangThaiTraPhiTre]) VALUES (5088, 1007, 50000, CAST(N'2019-12-11' AS Date), 1)
INSERT [dbo].[PhieuThue] ([maPhieuThue], [maKhachHang], [tienCoc], [ngayThue], [trangThaiTraPhiTre]) VALUES (5089, 1007, 50000, CAST(N'2019-12-11' AS Date), 1)
INSERT [dbo].[PhieuThue] ([maPhieuThue], [maKhachHang], [tienCoc], [ngayThue], [trangThaiTraPhiTre]) VALUES (5090, 1007, 50000, CAST(N'2019-12-11' AS Date), 1)
INSERT [dbo].[PhieuThue] ([maPhieuThue], [maKhachHang], [tienCoc], [ngayThue], [trangThaiTraPhiTre]) VALUES (5091, 1007, 100000, CAST(N'2019-12-11' AS Date), 1)
SET IDENTITY_INSERT [dbo].[PhieuThue] OFF
SET IDENTITY_INSERT [dbo].[PhieuTra] ON 

INSERT [dbo].[PhieuTra] ([maPhieuTra], [ngayTra], [maCTPhieuThue]) VALUES (5101, CAST(N'2019-11-23' AS Date), 5113)
INSERT [dbo].[PhieuTra] ([maPhieuTra], [ngayTra], [maCTPhieuThue]) VALUES (5102, CAST(N'2019-11-23' AS Date), 5114)
INSERT [dbo].[PhieuTra] ([maPhieuTra], [ngayTra], [maCTPhieuThue]) VALUES (5103, CAST(N'2019-11-23' AS Date), 5112)
INSERT [dbo].[PhieuTra] ([maPhieuTra], [ngayTra], [maCTPhieuThue]) VALUES (5104, CAST(N'2019-12-11' AS Date), 5115)
INSERT [dbo].[PhieuTra] ([maPhieuTra], [ngayTra], [maCTPhieuThue]) VALUES (5105, CAST(N'2019-12-11' AS Date), 5116)
INSERT [dbo].[PhieuTra] ([maPhieuTra], [ngayTra], [maCTPhieuThue]) VALUES (5106, CAST(N'2019-12-11' AS Date), 5117)
INSERT [dbo].[PhieuTra] ([maPhieuTra], [ngayTra], [maCTPhieuThue]) VALUES (5107, CAST(N'2019-12-11' AS Date), 5118)
INSERT [dbo].[PhieuTra] ([maPhieuTra], [ngayTra], [maCTPhieuThue]) VALUES (5108, CAST(N'2019-12-12' AS Date), 5119)
SET IDENTITY_INSERT [dbo].[PhieuTra] OFF
SET IDENTITY_INSERT [dbo].[PhiTre] ON 

INSERT [dbo].[PhiTre] ([maPhiTre], [maPhieuTra], [tongTien], [tinhTrangThanhToan]) VALUES (5059, 5101, 25000, 1)
INSERT [dbo].[PhiTre] ([maPhiTre], [maPhieuTra], [tongTien], [tinhTrangThanhToan]) VALUES (5060, 5102, 25000, 1)
INSERT [dbo].[PhiTre] ([maPhiTre], [maPhieuTra], [tongTien], [tinhTrangThanhToan]) VALUES (5061, 5103, 50000, 1)
SET IDENTITY_INSERT [dbo].[PhiTre] OFF
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [tenDangNhap], [matKhau], [quyenTruyCap]) VALUES (1003, N'admin', N'admin', 1)
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [tenDangNhap], [matKhau], [quyenTruyCap]) VALUES (1007, N'nv0933323622', N'0933323622', 0)
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [tenDangNhap], [matKhau], [quyenTruyCap]) VALUES (1008, N'nv0933323621', N'0933323621', 0)
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [tenDangNhap], [matKhau], [quyenTruyCap]) VALUES (1009, N'nv0933323566', N'0933323566', 1)
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [tenDangNhap], [matKhau], [quyenTruyCap]) VALUES (1010, N'nv01667006543', N'01667006543', 1)
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
SET IDENTITY_INSERT [dbo].[TuaDe] ON 

INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1008, N'Hãy Trao Cho Anh', 10, 50000, N'Phim', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1009, N'Cơn Mưa Ngang Qua', 10, 50000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1010, N'Kẻ Huỷ Diệt', 10, 50000, N'Phim', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1011, N'Cơn Mưa Ngang Qua', 10, 50000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1012, N'Vượt Ngục', 10, 50000, N'Phim', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1013, N'Chiếc Khăn Gió Ấm', 10, 50000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1014, N'Âm Thầm Bên Em      ', 10, 100000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1015, N'Cô Bé Mùa Đông', 7, 50000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1016, N'Hải Ngoại', 10, 50000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1017, N'X-Man', 10, 70000, N'Phim', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1018, N'Supper-Man', 10, 70000, N'Phim', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1019, N'Hai Phượng', 10, 50000, N'Phim', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1020, N'Anh Khác Hay em Khác', 12, 50000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1021, N'Sai Lầm Của Anh', 10, 50000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1022, N'Trót yêu', 10, 50000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1023, N'Mã Lệ Tây Phi', 10, 50000, N'Phim', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1024, N'Em Muốn Ta Là Gì', 10, 50000, N'Nhạc', 1, 5)
INSERT [dbo].[TuaDe] ([maTuaDe], [tenTuaDe], [soLuong], [donGia], [moTa], [trangThai], [ngayChoThue]) VALUES (1025, N'Fast And Furious', 9, 50000, N'Phim', 1, 5)
SET IDENTITY_INSERT [dbo].[TuaDe] OFF
ALTER TABLE [dbo].[ChiTietPhieuThue]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietPh__maDia__2D27B809] FOREIGN KEY([maDia])
REFERENCES [dbo].[Dia] ([maDia])
GO
ALTER TABLE [dbo].[ChiTietPhieuThue] CHECK CONSTRAINT [FK__ChiTietPh__maDia__2D27B809]
GO
ALTER TABLE [dbo].[ChiTietPhieuThue]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietPh__maPhi__2E1BDC42] FOREIGN KEY([maPhieuThue])
REFERENCES [dbo].[PhieuThue] ([maPhieuThue])
GO
ALTER TABLE [dbo].[ChiTietPhieuThue] CHECK CONSTRAINT [FK__ChiTietPh__maPhi__2E1BDC42]
GO
ALTER TABLE [dbo].[Dia]  WITH CHECK ADD  CONSTRAINT [FK__Dia__maTuaDe__25869641] FOREIGN KEY([maTuaDe])
REFERENCES [dbo].[TuaDe] ([maTuaDe])
GO
ALTER TABLE [dbo].[Dia] CHECK CONSTRAINT [FK__Dia__maTuaDe__25869641]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_TaiKhoan] FOREIGN KEY([maTaiKhoan])
REFERENCES [dbo].[TaiKhoan] ([maTaiKhoan])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_TaiKhoan]
GO
ALTER TABLE [dbo].[PhieuThue]  WITH CHECK ADD  CONSTRAINT [FK__PhieuThue__maKha__2F10007B] FOREIGN KEY([maKhachHang])
REFERENCES [dbo].[KhachHang] ([maKhachHang])
GO
ALTER TABLE [dbo].[PhieuThue] CHECK CONSTRAINT [FK__PhieuThue__maKha__2F10007B]
GO
ALTER TABLE [dbo].[PhieuTra]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTra_ChiTietPhieuThue] FOREIGN KEY([maCTPhieuThue])
REFERENCES [dbo].[ChiTietPhieuThue] ([maCTPhieuThue])
GO
ALTER TABLE [dbo].[PhieuTra] CHECK CONSTRAINT [FK_PhieuTra_ChiTietPhieuThue]
GO
ALTER TABLE [dbo].[PhiTre]  WITH CHECK ADD  CONSTRAINT [FK_PhiTre_PhieuTra] FOREIGN KEY([maPhieuTra])
REFERENCES [dbo].[PhieuTra] ([maPhieuTra])
GO
ALTER TABLE [dbo].[PhiTre] CHECK CONSTRAINT [FK_PhiTre_PhieuTra]
GO
