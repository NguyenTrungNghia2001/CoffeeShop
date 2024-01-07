﻿create database QLCAFE

create table KHACHHANG
(
	[IDKH] INT IDENTITY (1, 1) NOT NULL,
	[TenKH] NVARCHAR (100) NULL,
	 [MALKH] INT NOT NULL,
	 [DiaChiKH] nvarchar (100) null,
	 [SDTKH] INT,
	 [EmailKH] NVARCHAR (100) UNIQUE NOT NULL,
	 [PassKH] NCHAR (50)
	 PRIMARY KEY CLUSTERED ([IDKH] ASC),
	 FOREIGN KEY ([MALKH]) REFERENCES LOAIKHACHHANG([MALKH])
);
ALTER TABLE KHACHHANG
add CONSTRAINT [FK_KHACHHANG_MALKH] FOREIGN KEY([MALKH]) REFERENCES LOAIKHACHHANG([MALKH])
    ON DELETE CASCADE;
create table LOAIKHACHHANG
(
	[MALKH] INT IDENTITY (1, 1) NOT NULL,
	[TenLKH] nvarchar(150) NOT NULL,
	[GIAMGIA] int,
	PRIMARY KEY CLUSTERED ([MALKH] ASC)

);

create table CUAHANG(
	[IDCH] INT IDENTITY (1, 1) NOT NULL,
	[DiaChiCH] nvarchar(100) UNIQUE NOT NULL,
	[SLNhanVien] INT NOT NULL,
	[DoanhThu] INT NOT NULL,
	[IDNV] INT,
	[MASP] INT,
	[CALAMVIEC] INT,
	[IDKHO] INT
	PRIMARY KEY CLUSTERED ([DiaChiCH]),
	FOREIGN KEY ([IDNV]) REFERENCES NHANVIEN([IDNV]),
	FOREIGN KEY ([CALAMVIEC]) REFERENCES CALAMVIEC([MACLV]),
	FOREIGN KEY ([MASP]) REFERENCES SANPHAM([MASP]),
	FOREIGN KEY ([IDKHO]) REFERENCES KHOHANG([IDKHO])

);

create table KHOHANG(
	[IDKHO] INT IDENTITY (1,1) NOT NULL,
	[IDNLKHO] INT,
	[TENNLKHO] NVARCHAR(50),
	PRIMARY KEY CLUSTERED ([IDKHO] ASC),
	FOREIGN KEY ([IDNLKHO]) REFERENCES NGUYENLIEUKHO([IDNLKHO])
);

create table NGUYENLIEUKHO(
	[IDNLKHO] INT IDENTITY (1, 1) NOT NULL,
	[TENNLKHO] NVARCHAR(150) NOT NULL,
	[IDLNL] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([IDNLKHO] ASC),
	[SOLUONGTON] INT,
	FOREIGN KEY ([IDLNL]) REFERENCES LOAINGUYENLIEU([IDLNL])
)

create table NHANVIEN
(
	[IDNV] INT IDENTITY (1, 1) NOT NULL,
	[HOVATEN] NVARCHAR(150) NOT NULL,
	[NGAYSINH] DATE nOT NULL,
	[SDT] INT NOT NULL,
	[EMAIL] NVARCHAR(50) NOT NULL,
	[DIACHI] NVARCHAR(150) NOT NULL,
	[CHUCVU] NVARCHAR(150) NOT NULL,
	[MUCLUONG] FLOAT NOT NULL,
	[PASSWORD] NVARCHAR(150) NOT NULL,
	PRIMARY KEY CLUSTERED ([IDNV] ASC)
);

create table CHITIETLUONGNHANVIEN
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[IDNV] INT,
	[TONGGIOCONG] INT,
	[THANHTIEN] INT,
	[KYLUONG] nvarchar(20),
	CONSTRAINT [FK_IDNV] FOREIGN KEY ([IDNV]) REFERENCES NHANVIEN([IDNV]),
	PRIMARY KEY CLUSTERED ([ID] ASC)
)

create table SANPHAM
(
	[MASP] INT IDENTITY (1, 1) NOT NULL,
	[TENSP] NVARCHAR(150) NOT NULL,
	[LOAISP] INT,
	[IDNL] INT,
	[SLUONG] INT,
	[HINHANH] nvarchar(150) NOT NULL,
	[GIASP] INT,
	PRIMARY KEY CLUSTERED ([MASP] ASC),
	FOREIGN KEY ([IDNL]) REFERENCES NGUYENLIEU([IDNL]),
	FOREIGN KEY ([LOAISP]) REFERENCES LOAISP([MALSP])
);

create table LOAISP
(
	[MALSP] INT IDENTITY (1, 1) NOT NULL,
	[TENLSP] NVARCHAR(150) NOT NULL,
	[MOTA] NVARCHAR(150),
	PRIMARY KEY CLUSTERED ([MALSP] ASC)

);

create table CALAMVIEC
(
	[MACLV] INT IDENTITY (1, 1) NOT NULL,
	[TENCLV] NVARCHAR(150) NOT NULL,
	[GIOBD] TIME NOT NULL,
	[GIOKT] TIME NOT NULL,
	PRIMARY KEY CLUSTERED ([MACLV] ASC)
)

create table HOADON
(
	[IDHD] INT IDENTITY (1, 1) NOT NULL,
	[IDNV] INT NOT NULL,
	[MAKH] INT,
	[NGAYTAO] DATETIME,
	[TONGTIEN] INT NOT NULL,
	[MAGIAMGIA] INT,
	[DIEMTL] INT,
	[TenLKH] nvarchar(150) NOT NULL,
	[GIAMGIA] int,
	PRIMARY KEY CLUSTERED ([IDHD] ASC),
	FOREIGN KEY ([IDNV]) REFERENCES NHANVIEN([IDNV]),
	FOREIGN KEY ([MAKH]) REFERENCES KHACHHANG([IDKH]),
	CONSTRAINT [FK_MAGIAMGIA] FOREIGN KEY ([MAGIAMGIA]) REFERENCES KHUYENMAI([IDKM])
)

create table CHITIETHOADON
(
	[ID] INT IDENTITY (1,1),
	[MASP] INT,
	[SOLUONG] INT,
	[IDHD] INT,
	[TONGTIEN] INT,
	FOREIGN KEY ([IDHD]) REFERENCES HOADON([IDHD]),
	FOREIGN KEY ([MASP]) REFERENCES SANPHAM([MASP]),
	PRIMARY KEY CLUSTERED ([ID] ASC)
)

create table NGUYENLIEU
(
	[IDNL] INT IDENTITY (1, 1) NOT NULL,
	[TENNL] NVARCHAR(150) NOT NULL,
	[IDLNL] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([IDNL] ASC),
	[SOLUONGTON] INT,
	FOREIGN KEY ([IDLNL]) REFERENCES LOAINGUYENLIEU([IDLNL])
);

create table LOAINGUYENLIEU
(
	[IDLNL] INT IDENTITY (1, 1) NOT NULL,
	[TenLNL] nvarchar(150) NOT NULL,
	PRIMARY KEY CLUSTERED ([IDLNL] ASC)
)

create table KHUYENMAI
(
	[IDKM] INT IDENTITY (1, 1) NOT NULL,
	[TENKM] NVARCHAR(50) NOT NULL,
	[GIAMGIA] FLOAT NOT NULL,
	PRIMARY KEY CLUSTERED ([IDKM] ASC)
);

create table ADMINuser
(
	[TENDANGNHAP] nvarchar(50),
	[MATKHAU] NVARCHAR(50)
);

create table CongThuc
(
	[IDCT] INT IDENTITY (1, 1) NOT NULL,
	[MASP] INT,
	[TENSP] NVARCHAR(50),
	[IDNL] INT,
	[SOLUONG] INT,
	PRIMARY KEY CLUSTERED ([IDCT] ASC),
	FOREIGN KEY ([MASP]) REFERENCES SANPHAM([MASP]),
	FOREIGN KEY ([IDNL]) REFERENCES NGUYENLIEU([IDNL])
);

create table GIOHANG 
(
	[ID] INT IDENTITY (1, 1) NOT NULL,
	[NGAYDAT] DATE NULL,
	[IDKH] INT NULL,
	[Trigia] MONEY CHECK ([Trigia] > 0),
	[AddressDeliverry] NVARCHAR (MAX) NULL,
	[Dagiao] bit default 1 null,
	[Ngaygiaohang] Smalldatetime,
	[TenNguoiNhan] nvarchar(Max),
	[DienThoaiNhan] nvarchar(15),
	[HTThanhToan] bit Default 0,
	[HTGiaoHang] bit Default 0,
	PRIMARY KEY CLUSTERED ([ID] ASC),
	FOREIGN KEY ([IDKH]) REFERENCES KHACHHANG([IDKH])
);

--LOẠI SP
SET IDENTITY_INSERT LOAISP on
insert into LOAISP(MALSP,TENLSP,MOTA)
values('01','Trà',null)

insert into LOAISP(MALSP,TENLSP,MOTA)
values('02','Cà phê',null)

insert into LOAISP(MALSP,TENLSP,MOTA)
values('03','Đá xay',null)
SET IDENTITY_INSERT LOAISP off

select * from LOAISP

--LOẠI NGUYÊN LIỆU
SET IDENTITY_INSERT LOAINGUYENLIEU on
insert into LOAINGUYENLIEU(IDLNL,TenLNL)
values('01',N'Trà')

insert into LOAINGUYENLIEU(IDLNL,TenLNL)
values('02',N'Cà phê')

insert into LOAINGUYENLIEU(IDLNL,TenLNL)
values('03',N'Sữa')

insert into LOAINGUYENLIEU(IDLNL,TenLNL)
values('04',N'Whipping cream')

insert into LOAINGUYENLIEU(IDLNL,TenLNL)
values('05',N'Bánh')
SET IDENTITY_INSERT LOAINGUYENLIEU off

select * from LOAINGUYENLIEU

--NGUYÊN LIỆU
SET IDENTITY_INSERT NGUYENLIEU on
--Trà
insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('101','01','Trà lài',20)

insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('102','01','Trà đen',50)

insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('103','01','Trà sen',20)

insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('104','01','Trà xanh',20)

insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('105','01','Trà oolong',20)

--Cà phê
insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('106','02','Cafe Arabica',30)

insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('107','02','Cafe Robusta',30)

insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('108','02','Cafe Culi',30)

--Sữa
insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('109','03',N'Sữa tươi',30)

insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('110','03',N'Sữa đặc',30)

--Whipping cream
insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('111','04',N'Kem béo',20)

--Bánh
insert into NGUYENLIEU(IDNL,IDLNL,TENNL,SOLUONGTON)
values('112','05',N'Bánh oreo',15)
SET IDENTITY_INSERT NGUYENLIEU off

select * from NGUYENLIEU

--NGUYÊN LIỆU KHO
SET IDENTITY_INSERT NGUYENLIEUKHO on
insert into NGUYENLIEUKHO(IDNLKHO,TENNLKHO,IDLNL,SOLUONGTON)
values('1',N'Trà lài','01','50')

insert into NGUYENLIEUKHO(IDNLKHO,TENNLKHO,IDLNL,SOLUONGTON)
values('2',N'Trà đen','01','100')

insert into NGUYENLIEUKHO(IDNLKHO,TENNLKHO,IDLNL,SOLUONGTON)
values('3',N'Trà sen','01','50')

insert into NGUYENLIEUKHO(IDNLKHO,TENNLKHO,IDLNL,SOLUONGTON)
values('4',N'Cà phê Arabica ','02','100')

insert into NGUYENLIEUKHO(IDNLKHO,TENNLKHO,IDLNL,SOLUONGTON)
values('5',N'Sữa tươi','03','40')

insert into NGUYENLIEUKHO(IDNLKHO,TENNLKHO,IDLNL,SOLUONGTON)
values('6',N'Bánh oreo','05','20')
SET IDENTITY_INSERT NGUYENLIEUKHO off

select * from NGUYENLIEUKHO


--SẢN PHẨM
SET IDENTITY_INSERT SANPHAM on
--Trà
insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('1','01','103',N'Trà vải sen','50','30000','tra-vai-hat-sen.jpg')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('2','01','102',N'Trà đào','50','30000','tra-thach-dao.jpg')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('3','01','101',N'Trà nhãn lài','20','40000','2b5d1adf170b2dee170078fa367cf785_output.jpeg')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('4','01','105',N'Trà oolong dâu','30','40000','1580714802_Thiết kế không tên (10) (1).png')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('5','01','104',N'Trà xoài chanh dây','40','35000','17_olong_dao_chanh_day.jpg')

--Cà phê
insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('6','02','106',N'Cà phê đen đá','20','15000','hinh1.png')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('7','02','106',N'Cà phê sữa đá','20','20000','hinh1.png')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('8','02','106',N'Bạc xỉu','20','20000','hinh1.png')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('9','02','107',N'Expresso','20','40000','hinh1.png')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('10','02','108',N'Latte','20','40000','hinh1.png')

--Đá xay
insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('11','03','112',N'Oreo đá xay','30','55000','hinh1.png')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('12','03','104',N'Matcha đá xay','20','55000','hinh1.png')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('13','03','104',N'Việt quất đá xay','40','55000','hinh1.png')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('14','03','106',N'Mocha đá xay','30','55000','hinh1.png')

insert into SANPHAM(MASP,LOAISP,IDNL,TENSP,SLUONG,GIASP,HINHANH)
values('15','03','104',N'Socola đá xay','10','55000','hinh1.png')
SET IDENTITY_INSERT SANPHAM off

select * from SANPHAM

--LOẠI KH
SET IDENTITY_INSERT LOAIKHACHHANG on
insert into LOAIKHACHHANG(MALKH,TenLKH,GIAMGIA)
values('001',N'Thường',null)

insert into LOAIKHACHHANG(MALKH,TenLKH,GIAMGIA)
values('002',N'Thân thiết','5')

insert into LOAIKHACHHANG(MALKH,TenLKH,GIAMGIA)
values('003',N'VIP','10')
SET IDENTITY_INSERT LOAIKHACHHANG off

select * from LOAIKHACHHANG 

--KHÁCH HÀNG
SET IDENTITY_INSERT KHACHHANG on
insert into KHACHHANG(IDKH,TenKH,MALKH,DiaChiKH,SDTKH,EmailKH,PassKH)
values('1001',N'Nguyen Hương','001',N'12 Trần Hưng Đạo B','0123456789','huong@gmail.com','Huong12345')

insert into KHACHHANG(IDKH,TenKH,MALKH,DiaChiKH,SDTKH,EmailKH,PassKH)
values('1002',N'Nguyễn Bí','002',N'128 Bùi Thị Xuân B','0123456799','bi@gmail.com','Bi12345')

insert into KHACHHANG(IDKH,TenKH,MALKH,DiaChiKH,SDTKH,EmailKH,PassKH)
values('1003',N'Dương Hiệp','003',N'8 Nguyễn Duy','0123456999','hiep@gmail.com','Hiep12345')
SET IDENTITY_INSERT LOAIKHACHHANG off

select * from KHACHHANG 

--NHÂN VIÊN
SET IDENTITY_INSERT NHANVIEN on
insert into NHANVIEN(IDNV,HOVATEN,NGAYSINH,SDT,EMAIL,DIACHI,CHUCVU,MUCLUONG,PASSWORD)
values('1',N'Nguyễn Văn A','1990-07-25','0158954762','vana@gmail.com',N'TP.HCM',N'Quản lý','8000000','vana12345')

insert into NHANVIEN(IDNV,HOVATEN,NGAYSINH,SDT,EMAIL,DIACHI,CHUCVU,MUCLUONG,PASSWORD)
values('2',N'Nguyễn Văn B','2002-06-20','0158954598','vanb@gmail.com',N'TP.HCM',N'Nhân viên bán hàng','5000000','vanb12345')

insert into NHANVIEN(IDNV,HOVATEN,NGAYSINH,SDT,EMAIL,DIACHI,CHUCVU,MUCLUONG,PASSWORD)
values('3',N'Nguyễn Văn C','2001-06-23','0158952763','vanc@gmail.com',N'Hà Nội',N'Nhân viên thu ngân','6000000','vanc12345')
SET IDENTITY_INSERT NHANVIEN off

select * from NHANVIEN 

--KHUYẾN MÃI
SET IDENTITY_INSERT KHUYENMAI on
insert into KHUYENMAI(IDKM,TENKM,GIAMGIA)
values('1',N'Tri ân khách hàng VIP','0.15')

insert into KHUYENMAI(IDKM,TENKM,GIAMGIA)
values('2',N'Tri ân khách hàng thân thiết','0.1')

insert into KHUYENMAI(IDKM,TENKM,GIAMGIA)
values('3',N'Tri ân khách hàng thường','0.05')
SET IDENTITY_INSERT KHUYENMAI off

select * from KHUYENMAI

--ADMIN USER
insert into ADMINuser(TENDANGNHAP,MATKHAU)
values(N'Admin','Admin12345')

select * from ADMINuser 

--CHI TIẾT LƯƠNG NHÂN VIÊN


--CÔNG THỨC


--GIỎ HÀNG


--CA LÀM VIỆC


--HÓA ĐƠN


--CHI TIẾT HÓA ĐƠN


--CỬA HÀNG


--KHO HÀNG