/*create table 
(
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_
	primary key ()
)
go
*/

/*create table 
(
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_
	primary key ()
)
go
*/
create database anco_test_import
use anco_test_import

select * from c_phidonhang where c_donhang_id = 'ff09c01904a94f5583f0a9d7ba8a7076'

--delete md_doitackinhdoanh
--select * from md_donggoi
--select (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + CAST(NEWID() as nvarchar(255)) )),32))


create table md_nhom
(
	md_nhom_id nvarchar(32),
	tennhom nvarchar(255),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_nhom_id
	primary key (md_nhom_id)
)
go

create table md_chitietnhom
(
	md_chitietnhom_id nvarchar(32),
	md_nhom_id nvarchar(32),
	manv nvarchar(32),
	nguoiquanly bit,
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_chitietnhom_id
	primary key (md_chitietnhom_id)
)
go



create table md_nganhang
(
	md_nganhang_id nvarchar(32),
	ma_nganhang nvarchar(255),
	ten_nganhang nvarchar(500),
	thongtin nvarchar(max),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_nganhang_id
	primary key (md_nganhang_id)
)
go

create table md_mailtemplate
(
	md_mailtemplate_id nvarchar(32),
	ten_template nvarchar(255),
	subject_mail nvarchar(max),
	content_mail nvarchar(max),
	use_for nvarchar(32),
	default_mail bit,
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_mailtemplate_id
	primary key (md_mailtemplate_id)
)
go


create table md_bientau
(
	md_bientau_id nvarchar(32),
	md_sanpham_id nvarchar(32),
	ma_sanpham_ref nvarchar(32),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_bientau_id
	primary key (md_bientau_id)
)
go


create table md_kieudang
(
	md_kieudang_id nvarchar(32),
	ma_kieudang nvarchar(255),
	ten_tv nvarchar(255),
	ten_ta nvarchar(255),
	ten_tv_dai nvarchar(255),
	ten_ta_dai nvarchar(255),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_kieudang_id
	primary key (md_kieudang_id)
)
go


create table md_chucnang
(
	md_chucnang_id nvarchar(32),
	ma_chucnang nvarchar(255),
	ten_tv nvarchar(255),
	ten_ta nvarchar(255),
	ten_tv_dai nvarchar(255),
	ten_ta_dai nvarchar(255),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_chucnang_id
	primary key (md_chucnang_id)
)
go


CREATE TABLE c_lichsubooknangluc(
	c_lichsubooknangluc_id nvarchar(32) NOT NULL,
	c_tuannangluc_id nvarchar(32) NULL,
	md_nhomnangluc_id nvarchar(32) NULL,
	c_donhang_id nvarchar(32) NULL,
	tuanthu int,
	dauma nvarchar(50) ,
	tenhehang nvarchar(50) ,
	sl_dat numeric(18, 2) ,
	ngaydukien datetime,
	
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_lichsubooknangluc_id
	primary key(c_lichsubooknangluc_id)
)
go



create table md_trongluong
(
	md_trongluong_id nvarchar(32) not null,
	ten_trongluong nvarchar(32),
	tile numeric(18,2),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_trongluong_id
	primary key (md_trongluong_id)
)
go



create table md_kichthuoc
(
	md_kichthuoc_id nvarchar(32) not null,
	ten_kichthuoc nvarchar(32),
	tile numeric(18,2),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_kichthuoc_id
	primary key (md_kichthuoc_id)
)



create table md_dongtien
(
	md_dongtien_id nvarchar(32),
	ma_iso	nvarchar(32),
	bieutuong	nvarchar(32),

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_dongtien_id
	primary key (md_dongtien_id)
)
go


create table md_tygia
(
	md_tygia_id	nvarchar(32),
	ten_tygia nvarchar(32),
	tu_dongtien_id	nvarchar(32),
	sang_dongtien_id	nvarchar(32),
	hieuluc_tungay	datetime,
	hieuluc_denngay	datetime,
	nhan_voi	numeric(18,2),
	chia_cho	numeric(18,2),

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_tygia_id
	primary key (md_tygia_id)
)
go

create table md_quocgia
(
	md_quocgia_id nvarchar(32),
	ma_quocgia nvarchar(32),
	ten_quocgia	nvarchar(max),

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_quocgia_id
	primary key (md_quocgia_id)
)
go

create table md_khuvuc
(
	md_khuvuc_id	nvarchar(32),
	ma_khuvuc	nvarchar(32),
	ten_khuvuc	nvarchar(max),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_khuvuc
	primary key (md_khuvuc_id)
)
go

create table md_donvitinhsanpham
(
	md_donvitinhsanpham_id nvarchar(32),
	ma_edi nvarchar(32),
	ten_dvt nvarchar(32),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_donvitinhsanpham_id
	primary key (md_donvitinhsanpham_id)
)
go

create table md_sanpham
(
	md_sanpham_id	nvarchar(32),
	ma_sanpham	nvarchar(255),
	md_kieudang_id nvarchar(32),
	md_chucnang_id nvarchar(32),
	ma_sanphamcu	nvarchar(32),
	mota_tiengviet	nvarchar(max),
	mota_tienganh	nvarchar(max),
	ma_vach	nvarchar(32),
	md_donvitinhsanpham_id	nvarchar(32), 
	l_inch	numeric(18,2) default 0,
	w_inch	numeric(18,2) default 0,
	h_inch	numeric(18,2) default 0,
	l_cm	numeric(18,2) default 0,
	w_cm	numeric(18,2) default 0,
	h_cm	numeric(18,2) default 0,
	trongluong	numeric(18,2) default 0,
	dientich numeric(18,2)default 0,
	md_nhomnangluc_id nvarchar(32),
	-- sanpham_docquyen	bit,
	md_chungloai_id	nvarchar(32),
	ghichu	nvarchar(255),
	nhacungung	nvarchar(32),
	vattu bit, 
	ban_thanhpham bit,
	sanpham bit default 1,
	md_cangbien_id nvarchar(32),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_sanpham_md_sanpham_id
	primary key (md_sanpham_id)
)
go


create table md_color_reference
(
	
	md_color_reference_id nvarchar(32),
	c_baogia_id nvarchar(32),
	mau nvarchar(32),
	url nvarchar(255),
	filter nvarchar(255),	
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_color_reference_md_color_reference_id
	primary key (md_color_reference_id)
)
go


create table md_cangxuathang
(
	md_cangxuathang_id nvarchar(32),
	md_sanpham_id nvarchar(32),
	md_cangbien_id nvarchar(32),
	macdinh bit,
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_cangxuathang_md_cangxuathang_id
	primary key (md_cangxuathang_id)
)
go

create table md_nhomnangluc
(
	md_nhomnangluc_id nvarchar(32),
	md_chungloai_id nvarchar(32), 
	hehang nvarchar(32),
	nhom nvarchar(10),
	mota_tiengviet nvarchar(max),
	hscode nvarchar(max),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_nhomnangluc_id
	primary key (md_nhomnangluc_id)
)
go

create table c_tuannangluc
(
	c_tuannangluc_id nvarchar(32),
	md_doitackinhdoanh_id nvarchar(32),
	ten_tuan nvarchar(50),
	tuanthu int,
	ngaybatdau datetime,
	nam int,
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_tuannangluc_c_tuannangluc_id
	primary key (c_tuannangluc_id)
)
go


create table c_chitietnangluc
(
	c_chitietnangluc_id nvarchar(32),
	md_nhomnangluc_id nvarchar(32),
	c_tuannangluc_id nvarchar(32),
	md_doitackinhdoanh_id nvarchar(32),
	tenhehang nvarchar(50),
	dauma nvarchar(50),
	thoigiannhan_dh int,
	nangxuat_tb numeric(18,2),
	nangxuat_min numeric(18,2),
	nangxuat_max numeric(18,2),
	sl_dadat numeric(18,2),
	sl_conlai numeric(18,2),
	tinhtrang nvarchar(50),
	
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_chitietnangluc_c_chitietnangluc_id
	primary key (c_chitietnangluc_id)
)
go

create table md_anhsanpham
(
	md_anhsanpham_id nvarchar(32) not null,
	url nvarchar(255),
	filter nvarchar(255),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_anhsanpham_md_anhsanpham_id
	primary key (md_anhsanpham_id)
)
go


/*create table md_loaisanpham
(
	md_loaisanpham_id	nvarchar(32),
	ma_loaisanpham	nvarchar(32),
	ten_loaisanpham	nvarchar(255),

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_loaisanpham_md_loaisanpham_id
	primary key (md_loaisanpham_id)
)
go*/
create table md_donvitinh
(
	md_donvitinh_id	nvarchar(32),
	ma_edi	nvarchar(255),
	ten_dvt	nvarchar(255),

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_donvitinh_id
	primary key (md_donvitinh_id)
)
go

create table md_doitackinhdoanh
(
	md_doitackinhdoanh_id	nvarchar(32),
	md_loaidtkd_id	nvarchar(32),
	md_quocgia_id	nvarchar(32),
	md_khuvuc_id nvarchar(32),
	ma_dtkd	nvarchar(512),
	ten_dtkd	nvarchar(512),
	daidien	nvarchar(255),
	chucvu	nvarchar(255),
	tel	nvarchar(50),
	fax	nvarchar(50),
	email	nvarchar(512),
	url	nvarchar(512),
	diachi	nvarchar(1000),
	md_banggia_id nvarchar(32),
	
	so_taikhoan	nvarchar(512),
	nganhang	nvarchar(512),
	masothue	nvarchar(512),
	tong_congno	numeric(18,2) default 0,
	isncc bit,
	islienhe bit,

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_doitackinhdoanh_id
	primary key (md_doitackinhdoanh_id)
)
go

create table md_nguoilienhe
(
	md_nguoilienhe_id	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32),
	ten nvarchar(250),
	diachi nvarchar(250),
	cmnd nvarchar(20),
	masothue nvarchar(20),
	email nvarchar(60),
	sdt nvarchar(20),
	fax nvarchar(20),
	doitaclienquan nvarchar(32),
	muchoahong int,
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_nguoilienhe_id
	primary key (md_nguoilienhe_id)
)
go


/*create table md_nanglucncu
(
	md_nanglucncu_id	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32),
	ten_nl	nvarchar(255),
	muc_toithieu	numeric(18,2) default 0,
	muc_trungbinh	numeric(18,2) default 0,
	muc_toida	numeric(18,2) default 0,

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_nanglucncu_md_nanglucncu_id
	primary key (md_nanglucncu_id)
)
go*/

create table md_loaidtkd
(
	md_loaidtkd_id	nvarchar(32),
	ma_loaidtkd	nvarchar(255),
	ten_loaidtkd	nvarchar(255),

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_loaidtkd_id
	primary key (md_loaidtkd_id)
)
go

create table md_hanghoadocquyen
(
	md_hanghoadocquyen_id	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32),
	md_sanpham_id	nvarchar(32),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_hanghoadocquyen_id
	primary key (md_hanghoadocquyen_id)
)
go


create table md_banggia
(
	md_banggia_id	nvarchar(32),
	ten_banggia	nvarchar(255),
	md_dongtien_id	nvarchar(32),
	banggiaban	bit,

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_banggia_id
	primary key (md_banggia_id)
)
go



create table md_phienbangia
(
	md_phienbangia_id	nvarchar(32),
	ten_phienbangia	nvarchar(32),
	md_banggia_id	nvarchar(32),
	ngay_hieuluc	datetime,
	md_trangthai_id nvarchar(32),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_phienbangia_id
	primary key (md_phienbangia_id)
)
go

create table md_giasanpham
(
	md_giasanpham_id	nvarchar(32),
	md_phienbangia_id	nvarchar(32),
	md_sanpham_id	nvarchar(32),
	gia	numeric(18,2),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_giasanpham_id
	primary key (md_giasanpham_id)
)
go



create table md_donggoi
(
	md_donggoi_id	nvarchar(32),
	ma_donggoi nvarchar(32),
	ten_donggoi	nvarchar(32),
	sl_inner	numeric(18),
	dvt_inner	nvarchar(32),
	l1	numeric(18,1) default 0,
	w1	numeric(18,1) default 0,
	h1	numeric(18,1) default 0,
	sl_outer	numeric(18) default 0,
	dvt_outer	nvarchar(32) default 0,
	l2_mix	numeric(18,2) default 0,
	w2_mix	numeric(18,2) default 0,
	h2_mix	numeric(18,2) default 0,
	v2	numeric(18,3) default 0,
	sl_cont_mix	numeric(18) default 0,
	soluonggoi_ctn	numeric(18) default 0,
	md_trongluong_id nvarchar(32),
	vd	numeric(18) default 0, -- vach dai 
	vn	numeric(18) default 0, -- vach ngan
	vl	numeric(18) default 0, -- vach lot
	ghichu_vachngan nvarchar(500),
	mix_chophepsudung	bit,
	--md_kieudonggoi_id	nvarchar(32),
	--md_hinhthucdonggoi_id	nvarchar(32),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_donggoi_id
	primary key (md_donggoi_id)
)
go


create table md_donggoisanpham
(
	md_donggoisanpham_id	nvarchar(32),
	md_donggoi_id	nvarchar(32),
	md_sanpham_id	nvarchar(32),
	soluong	numeric(18) default 1,
	macdinh	bit,

	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_donggoisanpham_id
	primary key (md_donggoisanpham_id)
)
go


/*create table md_hinhthucdonggoi
(
	md_hinhthucdonggoi_id nvarchar(32),
	ma_htdg nvarchar(32),
	ten_htdg nvarchar(255),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_hinhthucdonggoi_md_hinhthucdonggoi_id
	primary key (md_hinhthucdonggoi_id)
)
go

create table md_kieudonggoi
(
	md_kieudonggoi_id nvarchar(32),
	ma_kdg nvarchar(32),
	ten_kdg nvarchar(255),
	
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_kieudonggoi_md_kieudonggoi_id
	primary key (md_kieudonggoi_id)
)
go*/

create table md_kho
(
	md_kho_id nvarchar(32),
	ma_kho nvarchar(32),
	ten_kho nvarchar(255),
	-- --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_kho_id
	primary key (md_kho_id)
)
go

create table md_hangtrongkho
(
	md_hangtrongkho_id nvarchar(32),
	md_kho_id nvarchar(32),
	md_sanpham_id nvarchar(32),
	qty numeric(18,2) default 0,
	qty_pre numeric(18,2) default 0,
	md_donvitinh_id nvarchar(32),
	
	--  --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	constraint pk_md_hangtrongkho_id
	primary key (md_hangtrongkho_id)
)
go




CREATE TABLE vaitro
(
	mavt VARCHAR(32) NOT NULL,
	tenvt NVARCHAR(255) NOT NULL,
	
	--  --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	CONSTRAINT pk_vaitro_mavt
	PRIMARY KEY(mavt)
)
GO


CREATE TABLE phanquyen
(
	mapq VARCHAR(32) NOT NULL,
	mavt VARCHAR(32) NOT NULL,
	mamenu VARCHAR(32) NOT NULL,
	xem bit,
	them bit,
	sua bit,
	xoa bit,
	truyxuat bit,
	
	--  --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	CONSTRAINT pk_phanquyen_mapq
	PRIMARY KEY(mapq)
)
GO

CREATE TABLE menu
(
	mamenu varchar(32) NOT NULL,
	module_id varchar(32) NOT NULL,
	title nvarchar(255) NOT NULL,
	tenmenu nvarchar(255) NOT NULL,
	image_url nvarchar(255) NOT NULL,
	url nvarchar(255) NOT NULL,
	sortorder int NOT NULL,
	
	--  --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	CONSTRAINT pk_menu_mamenu
	PRIMARY KEY(mamenu)
)
GO

CREATE TABLE nhanvien
(
	manv varchar(32) NOT NULL,
	matkhau varchar(32) NOT NULL,
	mavt varchar(32) NOT NULL,
	
	--  --
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(2000),
	hoatdong bit
	
	CONSTRAINT pk_nhanvien_manv
	PRIMARY KEY(manv)
)
GO

-- ngay 05-06-2012 --

create table md_loaichungtu
(
	md_loaichungtu_id	nvarchar(32),
	tenchungtu	nvarchar(255),
	kieu_doituong	nvarchar(10),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_loaichungtu_id
	primary key (md_loaichungtu_id)
)
go


create table md_sochungtu
(
	md_sochungtu_id	nvarchar(32),
	md_loaichungtu_id nvarchar(32),
	ten_sochungtu	nvarchar(255),
	buocnhay	int,
	so_duocgan	numeric(18)  default 0,
	tiento	varchar(10),
	hauto	varchar(5),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_sochungtu_id
	primary key (md_sochungtu_id)
)
go


/*create table md_trangthai
(
	md_trangthai_id	nvarchar(32),
	md_loaitrangthai_id	nvarchar(32),
	ma_trangthai	nvarchar(50),
	tentrangthai	nvarchar(50),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_trangthai_md_trangthai_id
	primary key (md_trangthai_id)
)
go

create  table md_loaitrangthai
(
	md_loaitrangthai_id	nvarchar(32),
	ma_loaitt nvarchar(32),
	ten_loaitt	nvarchar(32),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_loaitrangthai_md_loaitrangthai_id
	primary key (md_loaitrangthai_id)
)
go*/

create table md_cangbien
(
	md_cangbien_id	nvarchar(32),
	ma_cangbien	nvarchar(255),
	ten_cangbien	nvarchar(255),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_cangbien_id
	primary key (md_cangbien_id)
)
go

create table md_paymentterm
(
	md_paymentterm_id	nvarchar(32),
	ma_paymentterm	nvarchar(255),
	ten_paymentterm	nvarchar(255),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_paymentterm_id
	primary key (md_paymentterm_id)
)
go

create table a_namtaichinh
(
	a_namtaichinh_id	nvarchar(32),
	ten_namtaichinh	nvarchar(255),
	nam int,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_a_namtaichinh_id
	primary key (a_namtaichinh_id)
)
go

create table a_kytrongnam
(
	a_kytrongnam_id	nvarchar(32),
	a_namtaichinh_id	nvarchar(32),
	soky	int,
	tenky	nvarchar(50),
	ngaybatdau	datetime,
	ngayketthuc	datetime,
	loaiky	nvarchar(32),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_a_kytrongnam_id
	primary key (a_kytrongnam_id)
)
go

create table a_dongmoky
(
	a_dongmoky_id	nvarchar(32),
	a_namtaichinh_id	nvarchar(32),
	a_kytrongnam_id	nvarchar(32),
	ky_hoatdong	nvarchar(32),
	daxuly bit,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_a_dongmoky_id
	primary key (a_dongmoky_id)
)
go


create table c_donmuabaobi
(
	c_donmuabaobi_id	nvarchar(32),
	donmua	nvarchar(32),
	ngaylap	datetime,
	nguoilap	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32),
	md_trangthai_id	nvarchar(32),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_donmuabaobi_id
	primary key (c_donmuabaobi_id)
)
go

/*create table c_dongbaobi
(
	c_dongbaobi_id	nvarchar(32),
	c_donmuabaobi_id	nvarchar(32),
	md_hinhthucdonggoi_id	nvarchar(32),
	quycach	nvarchar(32),
	soluong	numeric(18,2) default 0,
	gia	numeric(18,2) default 0,
	line	int default 0,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_dongbaobi_c_dongbaobi_id
	primary key (c_dongbaobi_id)
)
go

create table c_kiemkekho
(
	c_kiemkekho_id	nvarchar(32),
	ten_kiemke	nvarchar(255),
	md_kho_id	nvarchar(32),
	ngaykiemke	datetime,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_kiemkekho_c_kiemkekho_id
	primary key (c_kiemkekho_id)
)
go

create table c_dongkiemke
(
	c_dongkiemke_id	nvarchar(32),
	c_kiemkekho_id	nvarchar(32),
	md_sanpham_id	nvarchar(32),
	line	int,
	sl_demduoc	numeric(18,2) default 0,
	md_donvitinh_id	nvarchar(32),
	sl_sosach	numeric(18,2) default 0,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_dongkiemke_c_dongkiemke_id
	primary key (c_dongkiemke_id)
)
go*/


create table c_donhang
(
	c_donhang_id	nvarchar(32),
	c_baogia_id nvarchar(32),
	sochungtu	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32),
	ngaylap	datetime,
	nguoilap	nvarchar(32),
	md_cangbien_id	nvarchar(32),
	discount	numeric(18,2),
	shipmentdate	datetime,
	shipmenttime	datetime,
	md_paymentterm_id	nvarchar(32),
	md_trongluong_id	nvarchar(32),
	md_kichthuoc_id	nvarchar(32),
	payer	nvarchar(200),
	portdischarge	varchar(400),
	sl_cont numeric(18),
	loai_cont nvarchar(32),
	amount	numeric(18,2) default 0,
	totalamount	numeric(18,2) default 0,
	totalcbm	numeric(18,2) default 0,
	totalcbf	numeric(18,2) default 0,
	ismakepi	bit,
	daxuathang bit,
	dataoinvoice bit,
	md_banggia_id	nvarchar(32),
	md_trangthai_id	nvarchar(32),
	md_dongtien_id	nvarchar(32),
	md_nganhang_id nvarchar(32),
	line int,
	donhang_mau bit,
	dagui_mail bit,
	hoahong numeric(18, 0),
	md_nguoilienhe_id nvarchar(32),
	ngaydieuchinh datetime,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_donhang_id
	primary key (c_donhang_id)
)
go


create table c_dongdonhang
(
	c_dongdonhang_id	nvarchar(32),
	c_donhang_id	nvarchar(32),
	md_sanpham_id nvarchar(32) NOT NULL,
	mota_tiengviet nvarchar(max),
	mota_tienganh nvarchar(max),
	ma_sanpham_khach nvarchar(32),
	sothutu int, 
	md_donggoi_id nvarchar(32),
	giafob numeric(18,2) default 0,
	soluong numeric(18) default 0,
	soluong_dathang numeric(18) default 0,
	soluong_conlai numeric(18) default 0,
	soluong_daxuat numeric(18) default 0,
	sl_inner numeric(18) default 0,
	l1 numeric(18,1) default 0,
	w1 numeric(18,1) default 0,
	h1 numeric(18,1) default 0,
	sl_outer numeric(18) default 0,
	l2 numeric(18,1) default 0,
	w2 numeric(18,1) default 0,
	h2 numeric(18,1) default 0,
	v2 numeric(18,3) default 0,
	sl_cont numeric(18) default 0,
	vd	numeric(18,1) default 0, -- vach dai 
	vn	numeric(18,1) default 0, -- vach ngan
	vl	numeric(18,1) default 0, -- vach lot
	ghichu_vachngan nvarchar(500),
	ghichu nvarchar(255),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_dongdonhang_id
	primary key (c_dongdonhang_id)
)
go

create table c_phidonhang
(
	c_phidonhang_id	nvarchar(32),
	c_donhang_id	nvarchar(32),
	phi_tang numeric(18, 2),
	phi_giam numeric(18, 2),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_phidonhang_id
	primary key (c_phidonhang_id)
)
go


/*create table c_nanglucncu
(
	c_nanglucncu_id	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_nanglucncu_c_nanglucncu_id
	primary key (c_nanglucncu_id)
)
go*/

create table c_danhsachdathang
(
	c_danhsachdathang_id	nvarchar(32),
	sochungtu	nvarchar(32),
	ngaylap	datetime,
	md_doitackinhdoanh_id nvarchar(32),
	hangiaohang_po	datetime,
	nguoi_phutrach	nvarchar(32),
	nguoi_dathang	nvarchar(32),
	huongdanlamhang nvarchar(max),
	diachigiaohang nvarchar(max),
	c_donhang_id	nvarchar(32),
	so_po	nvarchar(32),
	md_trangthai_id	nvarchar(32),
	total numeric(18,2),
	isgui_hdlh bit,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_danhsachdathang_id
	primary key (c_danhsachdathang_id)
)
go


create table c_dongdsdh
(
	c_dongdsdh_id	nvarchar(32),
	c_danhsachdathang_id	nvarchar(32),
	c_dongdonhang_id	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32), -- nha cung ung
	huongdan_dathang	nvarchar(max),
	han_giaohang	datetime,
	tem_dan	nvarchar(255),
	sl_dathang numeric(18),
	sl_dagiao numeric(18),
	sl_conlai  numeric(18),
	gianhap	numeric(18,2) default 0,
	sothutu	int,
	
	md_sanpham_id	nvarchar(32),
	mota_tiengviet	nvarchar(255),
	mota_tienganh	nvarchar(255),
	ma_sanpham_khach nvarchar(100),
	md_donggoi_id nvarchar(32),
	sl_inner numeric(18) default 0,
	l1 numeric(18,1) default 0,
	w1 numeric(18,1) default 0,
	h1 numeric(18,1) default 0,
	sl_outer numeric(18) default 0,
	l2 numeric(18,1) default 0,
	w2 numeric(18,1) default 0,
	h2 numeric(18,1) default 0,
	v2 numeric(18,1) default 0,
	sl_cont numeric(18) default 0,
	vd	numeric(18,1) default 0, -- vach dai 
	vn	numeric(18,1) default 0, -- vach ngan
	vl	numeric(18,1) default 0, -- vach lot
	ghichu_vachngan nvarchar(500),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_dongdsdh_id
	primary key (c_dongdsdh_id)
)
go


create table c_dongkhxh
(
	c_dongkhxh_id	nvarchar(32),
	c_kehoachxuathang_id	nvarchar(32),
	c_donhang_id	nvarchar(32),
	c_dongdonhang_id	nvarchar(32),
	soluong	numeric(18,2) default 0,
	ngay_xuathang	datetime,
	line	int,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_dongkhxh_id
	primary key (c_dongkhxh_id)
)
go

create table c_packinginvoice
(
	c_packinginvoice_id	nvarchar(32),
	so_pkl	nvarchar(255),
	so_inv	nvarchar(255),
	--c_donhang_id	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32),
	
	ngaylap	datetime,
	nguoilap	nvarchar(32),
	consignee	nvarchar(255),
	notifparty	nvarchar(255),
	alsonotifyparty	nvarchar(255),
	mv	nvarchar(50),
	etd	datetime,
	noidi	nvarchar(32),
	noiden	nvarchar(32),
	blno	nvarchar(50),
	commodity	nvarchar(255),
	discount	numeric(18,2) default 0,
	handling_fee	numeric(18,2),
	diengiai_cong	nvarchar(200),
	giatri_cong	numeric(18,2) default 0,
	diengiai_tru	nvarchar(200),
	giatri_tru	numeric(18,2) default 0,
	commodityvn	nvarchar(255),
	ngay_motokhai	datetime,
	ngay_phaitt	datetime,
	totalnet	numeric(18,2) default 0,
	totalgross	numeric(18,2) default 0,
	totaldis	numeric(18,2) default 0,
	md_trangthai_id	nvarchar(32),
	tiendatcoc numeric(18,2),
	tiendatra  numeric(18,2),
	tienconlai  numeric(18,2),
	thanhtoanxong bit,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_packinginvoice_id
	primary key (c_packinginvoice_id)
)
go

create table c_dongpklinv
(
	c_dongpklinv_id	nvarchar(32),
	c_packinginvoice_id	nvarchar(32),
	c_donhang_id	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32),
	c_dongnhapxuat_id	nvarchar(32),
	md_sanpham_id	nvarchar(32),
	mota_tienganh	nvarchar(255),
	mota_tiengviet nvarchar(255),
	ma_sanpham_khach nvarchar(255),
	soluong	numeric(18) default 0,
	gia	numeric(18,2) default 0,
	thanhtien	numeric(18,2) default 0,
	line	int default 0,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_dongpklinv_c_dongpklinv_id
	primary key (c_dongpklinv_id)
)
go

create table c_nhapxuat 
(
	c_nhapxuat_id	nvarchar(32),
	c_donhang_id	nvarchar(32),
	sophieu	nvarchar(32),
	sophieunx	nvarchar(max),
	ngay_giaonhan	datetime,
	md_doitackinhdoanh_id	nvarchar(32),
	nguoigiao	nvarchar(32),
	nguoinhan	nvarchar(32),
	sophieukhach	nvarchar(32),
	ngay_phieu	datetime,
	md_kho_id	nvarchar(32),
	--nhapxuat_xuong_id	nvarchar(32),
	soseal	nvarchar(32),
	socontainer	nvarchar(32),
	loaicont	nvarchar(32),
	md_trangthai_id	nvarchar(32),
	md_loaichungtu_id	nvarchar(32),
	cr_invoice bit,
	cr_phieuxuat bit,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_nhapxuat_c_nhapxuat_id
	primary key (c_nhapxuat_id)
)
go

create table c_dongnhapxuat
(
	c_dongnhapxuat_id	nvarchar(32),
	dongnhapxuat_ref nvarchar(32),
	c_dongdsdh_id  nvarchar(32),
	c_nhapxuat_id	nvarchar(32),
	c_dongdonhang_id	nvarchar(32),
	md_sanpham_id	nvarchar(32),
	mota_tiengviet	nvarchar(255),
	-- mahangtho_id	nvarchar(32),
	md_donvitinh_id	nvarchar(32),
	sl_dathang 	numeric(18) default 0,
	slphai_nhapxuat	numeric(18) default 0,
	slthuc_nhapxuat	numeric(18) default 0,
	dongia	numeric(18,2) default 0,
	sokien_thucte	numeric(18) default 0,
	line	int,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_dongnhapxuat_c_dongnhapxuat_id
	primary key (c_dongnhapxuat_id)
)
go


create table c_thuchi
(
	c_thuchi_id	nvarchar(32),
	sophieu	nvarchar(32),
	md_doitackinhdoanh_id	nvarchar(32),
	nguoi_giaonop	nvarchar(50),
	ngay_giaonop	datetime,
	sotien	numeric(18,2) default 0,
	md_dongtien_id	nvarchar(32),
	tygia	numeric(18,2) default 0,
	lydo	nvarchar(255),
	loaiphieu	nvarchar(32) default 0,
	sochungtu	nvarchar(32),
	quydoi_vnd	numeric(18,2) default 0,
	tk_no	int default 0,
	tk_co	int default 0,
	tk_quy	int default 0,
	so_dadinhkhoan	numeric(18,2) default 0,
	so_chuadinhkhoan	numeric(18,2) default 0,
	so_dachiphi	numeric(18,2) default 0,
	md_trangthai_id	nvarchar(32),
	md_loaichungtu_id	nvarchar(32),
	tongcackhoan numeric(18, 2),
	tongkhoanphi numeric(18, 2),
	tienconlai numeric(18, 2),
	ispaymentin bit,
	ngaylapphieu datetime,
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_thuchi_c_thuchi_id
	primary key (c_thuchi_id)
)
go

create table c_chitietthuchi
(
	c_chitietthuchi_id	nvarchar(32),
	c_thuchi_id	nvarchar(32),
	tk_no	int,
	tk_co	int,
	sotien	numeric(18,2) default 0,
	quydoi	numeric(18,2) default 0,
	diengiai	nvarchar(255),
	obj_code	nvarchar(50),
	obj_id	int default 0,
	obj_num	nvarchar(50),
	c_donhang_id	nvarchar(32),
	c_packinginvoice_id nvarchar(32),
	isdatcoc bit,
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_chitietthuchi_c_chitietthuchi_id
	primary key (c_chitietthuchi_id)
)
go

create table c_chiphilienquan
(
	c_chiphilienquan_id	nvarchar(32),
	c_thuchi_id	nvarchar(32),
	c_chitietthuchi_id	nvarchar(32),
	tk_no	int default 0,
	tk_co	int default 0,
	sotien	numeric(18,2) default 0,
	quydoi	numeric(18,2) default 0,
	diengiai	nvarchar(255),
	obj_code	nvarchar(50),
	obj_id	int default 0,
	obj_num	nvarchar(50),
	c_donhang_id	nvarchar(32),
	c_packinginvoice_id nvarchar(32),

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_chiphilienquan_c_chiphilienquan_id
	primary key (c_chiphilienquan_id)
)
go




create table c_baogia
(
	c_baogia_id nvarchar(32) not null,
	sobaogia nvarchar(100),
	md_doitackinhdoanh_id nvarchar(32), -- md_doitackinhdoanh_id -- khach hang
	md_paymentterm_id nvarchar(32),
	shipmenttime int,
	md_banggia_id nvarchar(32), -- bang gia
	ngaybaogia datetime,
	ngayhethan datetime,
	md_trongluong_id nvarchar(32),
	md_kichthuoc_id	nvarchar(32),
	md_cangbien_id nvarchar(32), -- md_cangbien_id
	totalcbm numeric(18,2) default 0,
	totalcbf  numeric(18,2) default 0,
	totalquo numeric(18,2) default 0,
	md_trangthai_id nvarchar(32),
	md_dongtien_id nvarchar(32),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_baogia_c_baogia_id
	primary key (c_baogia_id)
)
go


create table c_chitietbaogia
(
	c_chitietbaogia_id nvarchar(32) NOT NULL,
	c_baogia_id nvarchar(32) NOT NULL,
	md_sanpham_id nvarchar(32) NOT NULL,
	ma_sanpham_khach nvarchar(32),
	mota_tiengviet nvarchar(max),
	mota_tienganh nvarchar(max),
	trongluong numeric(18, 2),
	md_cangbien_id nvarchar(32),
	
	sothutu int, 
	giafob numeric(18,2) default 0,
	soluong numeric(18) default 0,
	md_donggoi_id nvarchar(32),
	sl_inner numeric(18) default 0,
	l1 numeric(18,2) default 0,
	w1 numeric(18,2) default 0,
	h1 numeric(18,2) default 0,
	sl_outer numeric(18) default 0,
	l2 numeric(18,2) default 0,
	w2 numeric(18,2) default 0,
	h2 numeric(18,2) default 0,
	v2 numeric(18,3) default 0,
	sl_cont numeric(18) default 0,
	vd	numeric(18,2) default 0, -- vach dai 
	vn	numeric(18,2) default 0, -- vach ngan
	vl	numeric(18,2) default 0, -- vach lot
	ghichu_vachngan nvarchar(500),
	ghichu nvarchar(255),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_chitietbaogia_c_chitietbaogia_id
	primary key (c_chitietbaogia_id)
)
go


/*
create table c_kehoachxuathang
(
	c_kehoachxuathang_id nvarchar(32) NOT NULL,
	ten_kehoach nvarchar(255),
	ngaylap datetime,

	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_kehoachxuathang_c_kehoachxuathang_id
	primary key (c_kehoachxuathang_id)
)
go
*/



create table md_chungloai
(
	md_chungloai_id nvarchar(32),
	code_cl nvarchar(32),
	tv_ngan nvarchar(255),
	ta_ngan nvarchar(255),
	tv_dai nvarchar(255),
	ta_dai  nvarchar(255),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_chungloai_md_chungloai_id
	primary key (md_chungloai_id)
)
go

create table md_detai
(
	md_detai_id nvarchar(32),
	md_chungloai_id  nvarchar(32),
	code_cl nvarchar(32),
	code_dt nvarchar(32),
	tv_ngan nvarchar(255),
	ta_ngan nvarchar(255),
	tv_dai nvarchar(255),
	ta_dai  nvarchar(255),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_detai_md_detai_id
	primary key (md_detai_id)
)
go

create table md_mausac
(
	md_mausac_id nvarchar(32),
	md_chungloai_id nvarchar(32),
	md_detai_id nvarchar(32),
	code_mau nvarchar(32),
	code_cl nvarchar(32),
	code_dt nvarchar(32),
	tv_ngan nvarchar(255),
	ta_ngan nvarchar(255),
	tv_dai nvarchar(255),
	ta_dai  nvarchar(255),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_mausac_md_mausac_id
	primary key (md_mausac_id)
)
go


create table md_bocai
(
	md_bocai_id nvarchar(32),
	code_bc nvarchar(32),
	tv_ngan nvarchar(255),
	ta_ngan nvarchar(255),
	tv_dai nvarchar(255),
	ta_dai  nvarchar(255),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_md_bocai_md_bocai_id
	primary key (md_bocai_id)
)
go


create table c_xacnhan_xuatkho
(
	c_xacnhan_xuatkho_id nvarchar(32),
	sochungtu nvarchar(32),
	ngaylap datetime,
	nguoilap nvarchar(32),
	md_doitackinhdoanh_id nvarchar(32),
	md_kho_id nvarchar(32),
	so_po nvarchar(32),
	c_donhang_id nvarchar(32),
	so_cont numeric(18),
	so_seal numeric(18),
	loai nvarchar(32),
	ghichu nvarchar(max),
	md_trangthai_id nvarchar(32),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_xacnhan_xuatkho_id
	primary key (c_xacnhan_xuatkho_id)
)
go


create table c_dongxacnhan_xuatkho
(
	c_dongxacnhan_xuatkho_id nvarchar(32),
	c_xacnhan_xuatkho_id nvarchar(32),
	c_dongdonhang_id nvarchar(32),
	md_sanpham_id nvarchar(32),
	sl_po numeric(18),
	sl_yeucauxuat numeric(18),
	sl_thucxuat numeric(18),
	ma_sanpham_khach nvarchar(32),
	mota_tienganh nvarchar(max),
	dvt_sanpham nvarchar(32),
	md_donggoi_id nvarchar(32),
	so_bien nvarchar(32),
	ghichu nvarchar(max),
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_dongxacnhan_xuatkho_id
	primary key (c_dongxacnhan_xuatkho_id)
)
go

/*insert into c_kehoachxuathang(
	c_kehoachxuathang_id, c_donhang_id
	, md_doitackinhdoanh_id, so_po
	, cbm, shipmenttime
	, md_trangthai_id, ngaytao, ngaycapnhat, hoatdong)
select (SELECT (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5',  CAST(NEWID() as nvarchar(255))  + c_donhang_id) ),32)))
	, c_donhang_id, md_doitackinhdoanh_id
	, sochungtu, totalcbm, shipmenttime
	, 'SOANTHAO', GETDATE(), GETDATE(), 1 
from 
	c_donhang
where 
	c_donhang_id = N'e57610db9ba895f9bb8b68c1fefab78e'*/


    

	                                                    
create table c_kehoachxuathang
(
	c_kehoachxuathang_id nvarchar(32),
	c_donhang_id nvarchar(32), --
	md_doitackinhdoanh_id nvarchar(32), --
	so_po nvarchar(32), --
	chungloaihang  nvarchar(255),
	cbm numeric(18,2),
	shipmenttime datetime,
	c_danhsachdathang_id nvarchar(32),
	ngaygiaohang datetime,
	ngayxonghang datetime,
	cont20 numeric(18,0),
	cont40 numeric(18,0),
	cont40hc numeric(18,0),
	ngayxacnhantem datetime,
	ngayxacnhanbaobi datetime,
	ngaykiemhang datetime,
	ngayxuathang datetime,
	thang int,
	nam int,
	md_trangthai_id nvarchar(32),
	ghichu nvarchar(max),
	
	
	ngaytao datetime,
	nguoitao nvarchar(32),
	ngaycapnhat datetime,
	nguoicapnhat nvarchar(32),
	mota nvarchar(255),
	hoatdong bit
	
	constraint pk_c_kehoachxuathang_id
	primary key (c_kehoachxuathang_id)
)
go

select * from md_donggoi



alter proc CopyToXK
(
	@c_nhapxuat_id nvarchar(32)
)
as
begin
	declare @sophieu nvarchar(32);
	set @sophieu = (select (sct.tiento + '/' + CAST(sct.so_duocgan as nvarchar(32)) + '/' + sct.hauto) as sophieu 
                    from md_sochungtu sct, md_loaichungtu lct 
                    where sct.md_loaichungtu_id = lct.md_loaichungtu_id 
                    AND lct.kieu_doituong = 'XK')
	
	declare @newid nvarchar(32);
	set @newid = (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(NEWID() as nvarchar(255)) +  @c_nhapxuat_id)),32)) 
	
	-- insert c_nhapxuat
	INSERT INTO [c_nhapxuat]
           ([c_nhapxuat_id],[c_donhang_id],[sophieu]
           ,[ngay_giaonhan],[md_doitackinhdoanh_id]
           ,[nguoigiao],[nguoinhan],[sophieukhach]
           ,[ngay_phieu],[md_kho_id],[soseal]
           ,[socontainer],[loaicont],[md_trangthai_id]
           ,[md_loaichungtu_id],[ngaytao],[nguoitao]
           ,[ngaycapnhat],[nguoicapnhat],[mota]
           ,[hoatdong])
	SELECT @newid ,[c_donhang_id],@sophieu
           ,[ngay_giaonhan],[md_doitackinhdoanh_id]
           ,[nguoigiao],[nguoinhan],[sophieukhach]
           ,[ngay_phieu], [md_kho_id], [soseal]
           ,[socontainer],[loaicont],'SOANTHAO'
           ,'XK',GETDATE(),[nguoitao]
           ,GETDATE(),[nguoicapnhat],[mota]
           ,[hoatdong]
	FROM c_nhapxuat WHERE c_nhapxuat_id = @c_nhapxuat_id
		
	-- insert c_dongnhapxuat
	INSERT INTO [c_dongnhapxuat]
           ([c_dongnhapxuat_id],[c_nhapxuat_id],[c_dongdonhang_id]
           ,[md_sanpham_id],[mota_tiengviet] ,[md_donvitinh_id]
           ,[slphai_nhapxuat] ,[slthuc_nhapxuat]
           ,[dongia] ,[sokien_thucte] ,[line]
           ,[ngaytao] ,[nguoitao] ,[ngaycapnhat]
           ,[nguoicapnhat] ,[mota] ,[hoatdong]
           ,[c_dongdsdh_id] ,[sl_dathang])
     SELECT (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(NEWID() as nvarchar(255)) +  c_dongnhapxuat_id)),32)) 
		   ,@newid,[c_dongdonhang_id]
           ,[md_sanpham_id],[mota_tiengviet] ,[md_donvitinh_id]
           ,[slphai_nhapxuat] ,[slthuc_nhapxuat]
           ,[dongia] ,[sokien_thucte] ,[line]
           ,[ngaytao] ,[nguoitao] ,[ngaycapnhat]
           ,[nguoicapnhat] ,[mota] ,[hoatdong]
           ,[c_dongdsdh_id] ,[sl_dathang]
	  FROM c_dongnhapxuat where c_nhapxuat_id = @c_nhapxuat_id
end
go



create trigger TrigUpdateSoPhieuXuatKho
	ON c_nhapxuat
	for insert
as
begin
	update
		 md_sochungtu 
	set 
		so_duocgan = so_duocgan + buocnhay 
	where 
		md_loaichungtu_id = (select md_loaichungtu_id from md_loaichungtu where kieu_doituong = N'XK' and hoatdong = 1)
end
go



CREATE TRIGGER TrigUpdateSoBaoGia
   ON  c_baogia
   AFTER INSERT
AS 
BEGIN
	update
		 md_sochungtu 
	set 
		so_duocgan = so_duocgan + buocnhay 
	where 
		md_loaichungtu_id = (select md_loaichungtu_id from md_loaichungtu where kieu_doituong = N'QO')
END
GO





CREATE TRIGGER TrigUpdateSoDonHang
   ON  c_donhang
   AFTER INSERT
AS 
BEGIN
	update
		 md_sochungtu 
	set 
		so_duocgan = so_duocgan + buocnhay 
	where 
		md_loaichungtu_id = (select md_loaichungtu_id from md_loaichungtu where kieu_doituong = N'PO')
END
GO


CREATE TRIGGER TrigUpdateSoPI
   ON  c_danhsachdathang
   AFTER INSERT
AS 
BEGIN
	update
		 md_sochungtu 
	set 
		so_duocgan = so_duocgan + buocnhay 
	where 
		md_loaichungtu_id = (select md_loaichungtu_id from md_loaichungtu where kieu_doituong = N'PI')
END
GO



ALTER TRIGGER updateCBMCBFQO
   ON  dbo.c_chitietbaogia
   FOR INSERT,DELETE,UPDATE
AS 
BEGIN
	DECLARE @baogia_id nvarchar(32)
	IF exists (SELECT * FROM Inserted)
		BEGIN
			SET @baogia_id = (SELECT distinct c_baogia_id FROM Inserted)
			UPDATE c_baogia SET totalcbm = round((select sum(l2 * h2 * w2/1000000 * soluong/sl_outer) FROM c_chitietbaogia
											 WHERE c_baogia_id = @baogia_id GROUP BY c_baogia_id),2),
							totalquo = round((select sum(soluong * giafob) from c_chitietbaogia
											 WHERE c_baogia_id = @baogia_id GROUP BY c_baogia_id),2)				 
		END

	IF exists (SELECT * FROM Deleted)
		BEGIN
			SET @baogia_id = (select distinct c_baogia_id FROM Deleted)
			UPDATE c_baogia SET totalcbm = round((select sum(l2 * h2 * w2/1000000 * soluong/sl_outer) FROM c_chitietbaogia
											 WHERE c_baogia_id = @baogia_id GROUP BY c_baogia_id),2),
								totalquo = round((select sum(soluong * giafob) from c_chitietbaogia
											 WHERE c_baogia_id = @baogia_id GROUP BY c_baogia_id),2)
		END
	
	IF(COLUMNS_UPDATED())is not null
		BEGIN
			SELECT @baogia_id = c_baogia_id FROM Deleted
			UPDATE c_baogia SET totalcbm = round((select sum(l2 * h2 * w2/1000000 * soluong/sl_outer) FROM c_chitietbaogia
											WHERE c_baogia_id = @baogia_id GROUP BY c_baogia_id),2),
								totalquo = round((select sum(soluong * giafob) from c_chitietbaogia
											 WHERE c_baogia_id = @baogia_id GROUP BY c_baogia_id),2)
		END
END
GO



ALTER TRIGGER updateCBMCBFPO
   ON  dbo.c_dongdonhang
   FOR INSERT,DELETE,UPDATE
AS 
BEGIN
	DECLARE @donhang_id nvarchar(32)
	IF exists (SELECT * FROM Inserted)
		BEGIN
			SET @donhang_id = (SELECT distinct c_donhang_id FROM Inserted)
			UPDATE c_donhang SET totalcbm = round((select sum(l2 * h2 * w2/1000000 * soluong/sl_outer) FROM c_dongdonhang
											 WHERE c_donhang_id = @donhang_id GROUP BY c_donhang_id),2),
							amount = round((select sum(soluong * giafob) from c_dongdonhang
											 WHERE c_donhang_id = @donhang_id GROUP BY c_donhang_id),2)				 
		END

	IF exists (SELECT * FROM Deleted)
		BEGIN
			SET @donhang_id = (SELECT distinct c_donhang_id FROM Deleted)
			UPDATE c_donhang SET totalcbm = round((select sum(l2 * h2 * w2/1000000 * soluong/sl_outer) FROM c_dongdonhang
											 WHERE c_donhang_id = @donhang_id GROUP BY c_donhang_id),2),
								amount = round((select sum(soluong * giafob) from c_dongdonhang
											 WHERE c_donhang_id = @donhang_id GROUP BY c_donhang_id),2)
		END
	
	IF(COLUMNS_UPDATED())is not null
		BEGIN
			SELECT @donhang_id = c_donhang_id FROM Deleted
			UPDATE c_donhang SET totalcbm = round((select sum(l2 * h2 * w2/1000000 * soluong/sl_outer) FROM c_dongdonhang
											WHERE c_donhang_id = @donhang_id GROUP BY c_donhang_id),2),
								amount = round((select sum(soluong * giafob) from c_dongdonhang
											 WHERE c_donhang_id = @donhang_id GROUP BY c_donhang_id),2)
		END
END
GO

alter proc QuotationToPO
(
	@c_baogia_id nvarchar(32)
)
as
begin
	declare @c_donhang_id nvarchar(32)
	declare @sochungtu nvarchar(32)
	
	set @sochungtu = (	select (sct.tiento + '/' + CAST(sct.so_duocgan as nvarchar(32)) + '/' + hauto) as sochungtu 
						 from 
							md_sochungtu sct, md_loaichungtu lct 
						 WHERE 
							sct.md_loaichungtu_id = lct.md_loaichungtu_id AND lct.kieu_doituong = N'PO'
					  )	
					  
	set @c_donhang_id =  (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + @sochungtu)),32)) 
	
	
    
		-- insert to c_donhang
		INSERT INTO [c_donhang] 
				(
					[c_donhang_id],[c_baogia_id],[sochungtu]
					,[md_doitackinhdoanh_id],[ngaylap],[nguoilap],[md_cangbien_id]
					,[discount],[shipmentdate],[shipmenttime] ,[md_paymentterm_id]
					,[md_trongluong_id],[md_kichthuoc_id],[payer],[portdischarge]
					,[amount],[totalcbm],[totalcbf],[ismakepi],[md_banggia_id]
					,[md_trangthai_id],[md_dongtien_id]
					,[ngaytao] ,[ngaycapnhat],[hoatdong]
				)
		select 
					@c_donhang_id, [c_baogia_id],  @sochungtu, [md_doitackinhdoanh_id]
					, GETDATE(), N'người lập', [md_cangbien_id], 0, GETDATE(), [shipmenttime]
					, [md_paymentterm_id], [md_trongluong_id], [md_kichthuoc_id]
					, N'người thanh toán', N'', [totalquo],[totalcbm],[totalcbf], 0
					,[md_banggia_id], N'SOANTHAO', [md_dongtien_id]
					,GETDATE(), GETDATE(), 1 
		from 
			c_baogia 
		where 
			c_baogia_id = @c_baogia_id
		-- # insert to c_donhang
	
		;
		
		-- insert to c_dongdonhang
		INSERT INTO [c_dongdonhang] 
				(
					[c_dongdonhang_id],[c_donhang_id]
					,[md_sanpham_id],[ma_sanpham_khach]
					,[sothutu],[giafob],[soluong]
					,[sl_inner],[l1],[w1],[h1],[sl_outer],[l2],[w2]
					,[h2],[v2],[sl_cont],[vd],[vn],[vl]
					,[ghichu_vachngan],[ngaytao],[ngaycapnhat],[hoatdong]
				)
		SELECT (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + [c_chitietbaogia_id])),32)) 
			, @c_donhang_id,[md_sanpham_id]
			, [ma_sanpham_khach],[sothutu],[giafob],[soluong]
			, [sl_inner],[l1],[w1],[h1],[sl_outer],[l2],[w2]
			, [h2],[v2],[sl_cont],[vd],[vn],[vl],[ghichu_vachngan]
			, GETDATE(), GETDATE(), 1 
		FROM 
			[c_chitietbaogia] 
		WHERE c_baogia_id = @c_baogia_id
		-- # insert to c_dongdonhang
		
	
	
end
go



alter proc PoToPi
(
	@c_donhang_id nvarchar(32)
)
as
begin

	declare @c_dsdathang_id nvarchar(32)
	declare @sochungtu nvarchar(32)
	
	-- tao so chung tu tu dong cho danh sach dat hang
	set @sochungtu = (	select (sct.tiento + '/' + CAST(sct.so_duocgan as nvarchar(32)) + '/' + hauto) as sochungtu 
						 from 
							md_sochungtu sct, md_loaichungtu lct 
						 WHERE 
							sct.md_loaichungtu_id = lct.md_loaichungtu_id AND lct.kieu_doituong = N'PI'
					  )	
					  
	-- tao id md5 cho danh sach dat hang					  
	set @c_dsdathang_id =  (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + @sochungtu)),32))
	
	
	insert into c_danhsachdathang 
		(
			c_danhsachdathang_id, sochungtu, ngaylap
			, hangiaohang_po, nguoi_phutrach
			, nguoi_dathang, c_donhang_id
			, md_trangthai_id, ngaytao, ngaycapnhat
			, hoatdong
		)
	select 
			@c_dsdathang_id, @sochungtu, GETDATE()
			, shipmentdate, N'Người phụ trách'
			, N'Người đặt hàng', c_donhang_id 
			, N'SOANTHAO', GETDATE(), GETDATE(), 1
	from 
		c_donhang 
	where 
		c_donhang_id = @c_donhang_id
		
	
	
	
		insert into c_dongdsdh
			(
				c_dongdsdh_id, c_danhsachdathang_id, c_dongdonhang_id
				, md_sanpham_id, mota_tiengviet, mota_tienganh
				, md_doitackinhdoanh_id, huongdan_dathang, han_giaohang, tem_dan
				, sl_dathang, sl_dagiao, sl_conlai, gianhap, sothutu
				, ngaytao, ngaycapnhat, hoatdong
			)
		select 
			(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(GETDATE() as nvarchar(32)) + c_dongdonhang_id)),32)) 
			, @c_dsdathang_id, c_dongdonhang_id, md_sanpham_id
			, (select mota_tiengviet from md_sanpham sp where sp.md_sanpham_id = ddh.md_sanpham_id)
			, (select mota_tienganh from md_sanpham sp where sp.md_sanpham_id = ddh.md_sanpham_id)
			, (select nhacungung from md_sanpham sp where sp.md_sanpham_id = ddh.md_sanpham_id)
			, N'Hướng dẫn đặt hàng',  GETDATE(), N'Tem dán', 0, 0, 0, 0, 10
			, GETDATE(), GETDATE(), 1
		from
			c_dongdonhang ddh
		where
			c_donhang_id = @c_donhang_id
	
end
go





create proc [dbo].[crtTuanNangLuc](@ncc_id nvarchar(32), @user_id nvarchar(32))
AS
BEGIN 
	DECLARE @tuanhientai int, @namhientai int, @startdate datetime
	SET @tuanhientai = (select datepart(wk, getdate()))
	SET @namhientai = (select datepart(year, getdate()))
	SET @startdate = cast((select (DATEADD(dd, -(DATEPART(dw, getdate())-2), getdate())))	as datetime)
	WHILE (@tuanhientai <= 52)
		BEGIN
			INSERT INTO [esc_anco].[dbo].[c_tuannangluc]
			   ([c_tuannangluc_id],[md_doitackinhdoanh_id],[ten_tuan],[tuanthu],[ngaybatdau],[hoatdong]
			   ,[ngaytao],[nguoitao],[ngaycapnhat],[nguoicapnhat],[nam])
			VALUES
			   (upper((select replace(newid(), '-', ''))), @ncc_id, (N'Tuần '+ cast(@tuanhientai as nvarchar)), @tuanhientai,@startdate,1
			   ,getdate(),@user_id,getdate() ,@user_id,@namhientai);
			   
			SET @tuanhientai = @tuanhientai+1;  
			SET @startdate = @startdate + 7; 
		END
END




INSERT INTO [c_dongdonhang]              
	([c_dongdonhang_id],[c_donhang_id],[md_sanpham_id]              
	,[ma_sanpham_khach],[sothutu],[giafob]              
	,[soluong],[sl_inner],[l1],[w1]              
	,[h1],[sl_outer],[l2]              
	,[w2],[h2],[v2]              
	,[sl_cont],[vd],[vn],[vl]              
	,[ghichu_vachngan]              
	,[ngaytao],[ngaycapnhat],[hoatdong])           
SELECT (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', N'c9f0c03ed4e5c08ad1b98fe8a61e07a3' + [c_chitietbaogia_id])),32))
	, N'a930f348ac21cd4c060996f32f6f48a5'
	,[md_sanpham_id]                 
	,[ma_sanpham_khach],[sothutu],[giafob]                 
	,[soluong],[sl_inner],[l1],[w1]                 
	,[h1],[sl_outer],[l2]                 
	,[w2],[h2],[v2]                 
	,[sl_cont],[vd],[vn],[vl]                 
	,[ghichu_vachngan]                 
	, GETDATE(), GETDATE(), 1             
FROM [c_chitietbaogia] where c_baogia_id = N'0f9b750f8e4c168cdb60274ade808e3c'













-- insert md_donvitinh -- 
insert into md_donvitinh(md_donvitinh_id, ma_edi,ten_dvt,ngaytao,ngaycapnhat,hoatdong)
select distinct (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', DVTO)),32)),  DVTO, DVTO, GETDATE(),GETDATE(), 1 from ancodb.dbo.dmCachDongGoi WHERE DVTO != '0'
--/ insert md_donvitinh -- 





-- insert loai san pham --
insert into 
	md_loaisanpham
		( md_loaisanpham_id,
		  ma_loaisanpham,ten_loaisanpham,
		  ngaytao,ngaycapnhat,hoatdong
		)
select 
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', maso)),32)),
	maso,
	RTRIM(LTRIM(SUBSTRING(SUBSTRING(ten,5, LEN(ten)),0,CHARINDEX('=',SUBSTRING(ten,5, LEN(ten)))))),
	GETDATE(), GETDATE(), 1
from  
	ancodb.dbo.dmChatLieu 
where 
	RTRIM(LTRIM(SUBSTRING(SUBSTRING(ten,5, LEN(ten)),0,CHARINDEX('=',SUBSTRING(ten,5, LEN(ten)))))) <> ''
	--AND LEN(RTRIM(LTRIM(SUBSTRING(SUBSTRING(ten,5, LEN(ten)),0,CHARINDEX('=',SUBSTRING(ten,5, LEN(ten))))))) <= 32
-- / insert loai san pham --

-- insert quoc gia, khu vuc --
insert into md_quocgia(md_quocgia_id,ma_quocgia,ten_quocgia,ngaytao,ngaycapnhat,hoatdong)
values((select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'VN')),32)), N'VN', N'VN', GETDATE(), GETDATE(), 1)

insert into md_khuvuc(md_khuvuc_id,ma_khuvuc,ten_khuvuc,ngaytao,ngaycapnhat,hoatdong)
values((select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'EU')),32)), N'EU', N'EU', GETDATE(), GETDATE(), 1)
-- / insert quoc gia, khu vuc --


-- insert loai doi tac kinh doanh -- 
insert into md_loaidtkd(md_loaidtkd_id, ma_loaidtkd,ten_loaidtkd, ngaytao,ngaycapnhat,hoatdong)
values((select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'NCC')),32)), N'NCC', N'Nhà Cung Cấp', GETDATE(),GETDATE(), 1)

insert into md_loaidtkd(md_loaidtkd_id, ma_loaidtkd,ten_loaidtkd, ngaytao,ngaycapnhat,hoatdong)
values((select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'KH')),32)), N'KH', N'Khách Hàng', GETDATE(),GETDATE(), 1)
--/ insert loai doi tac kinh doanh -- 



-- insert doi tac kinh doanh -- 
insert into md_doitackinhdoanh(
	md_doitackinhdoanh_id, 
	md_loaidtkd_id, 
	ma_dtkd,
	ten_dtkd,
	isncc, 
	md_quocgia_id,
	md_khuvuc_id)
select 
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(idkhachhang as nvarchar(32)))),32)),
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'NCC')),32)),
	Maso,
	TEN,
	1,
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'VN')),32)),
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'EU')),32))
from ancodb.dbo.dmKhachhang WHERE Loai = 1



insert into md_doitackinhdoanh(
	md_doitackinhdoanh_id, 
	md_loaidtkd_id, 
	ma_dtkd,
	ten_dtkd,
	isncc, 
	md_quocgia_id,
	md_khuvuc_id)
select 
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(idkhachhang as nvarchar(32)))),32)),
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'KH')),32)),
	Maso,
	TEN,
	0,
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'VN')),32)),
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', 'EU')),32))
from ancodb.dbo.dmKhachhang WHERE Loai <> 1


update md_doitackinhdoanh set ngaycapnhat = GETDATE(), ngaytao = GETDATE(), hoatdong = 1, tong_congno = 0
-- / insert doi tac kinh doanh -- 




INSERT INTO [esc_anco].[dbo].[md_sanpham]
           ([md_sanpham_id]
           ,[ma_sanpham]
           ,[mota_tiengviet]
           ,[mota_tienganh]
           ,[md_donvitinh_id]
           ,[l_cm]
           ,[w_cm]
           ,[h_cm]
           ,[l_inch]
           ,[w_inch]
           ,[h_inch]
           ,[trongluong]
           ,[cmp_master]
           ,[cmp_park]
           ,[md_loaisanpham_id]
           ,[nhacungung]
           ,[vattu]
           ,[ban_thanhpham]
           ,[sanpham]
           ,[ngaytao]
           ,[ngaycapnhat]
           ,[hoatdong])
select 
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(MaHH as nvarchar(32)))),32)),
	MaHH, TenViet, TenAnh, '0216903bb8e808d6cc91451b8125566c',
	ISNULL(L,0), ISNULL(W,0), ISNULL(H,0), L * 0.393700787, W * 0.393700787, H * 0.393700787, 0, 0, 0, '03afdbd66e7929b125f8597834fa83a4', '031f43e51da3f4e137bbce1b4ff13dd3',
	0,0,1, GETDATE(), GETDATE(), 1
from ancodb.dbo.dmHangHoa

insert md_trangthai(md_trangthai_id, md_loaitrangthai_id, ma_trangthai,tentrangthai, ngaytao,ngaycapnhat, hoatdong)
select 
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(Id as nvarchar(32)))),32)),
	'52f5e356536f02fcac9fd9a72d7c553b',
	CAST(Ma as nvarchar(32)), Ten,GETDATE(), GETDATE(), 1
from 
	ancodb.dbo.dmtrangthai





insert into md_giasanpham(md_giasanpham_id, md_phienbangia_id, md_sanpham_id, gia, ngaytao, ngaycapnhat, hoatdong)
select 
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(IDGia as nvarchar(32)))),32)),
	'02007b8f0f97bfae835921f91278a6a9',
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(MaHH as nvarchar(32)))),32)),
	ISNULL(Gia, 0), GETDATE(), GETDATE(), 1
from ancodb.dbo.dmGia









-- insert md_dongoi
INSERT INTO [esc_anco].[dbo].[md_donggoi]
           ([md_donggoi_id]
           ,[ten_donggoi]
           ,[sl_inner]
           ,[dvt_inner]
           ,[l1]
           ,[w1]
           ,[h1]
           ,[sl_outer]
           ,[dvt_outer]
           ,[l2_mix]
           ,[w2_mix]
           ,[h2_mix]
           ,[v2]
           ,[sl_cont_mix]
           ,[soluonggoi_ctn]
           ,[md_trongluong_id]
           ,[mix_chophepsudung]
           ,[md_kieudonggoi_id]
           ,[md_hinhthucdonggoi_id]
           ,[ngaytao]
           ,[ngaycapnhat]
           ,[hoatdong])
select 
	 (select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(IDDongGoi as nvarchar(32)))),32)),
	 Ten, 
	 SlInner,
	 DVTI,
	 L1,
	 W1,
	 H1, 
	 SlOuter,
	 DVTO,
	 L2,
	 W2,
	 H2,
	 V2, 
	 0, 
	 SLCont, 
	 0, 
	 0,
	 '179a20e66aef26b9e97f7cf43291dd9f', 
	 '82aebac8dfd6124bbb19915c5e6ea602',
	 GETDATE(), 
	 GETDATE(), 
	 1
from ancodb.dbo.dmCachDongGoi
GO
-- insert md_dongoi

update md_donggoi set dvt_inner = '08f01bca5e1ecf1466019d555f8b91a0', dvt_outer = '08f01bca5e1ecf1466019d555f8b91a0'



insert into md_donggoisanpham(md_donggoisanpham_id, md_donggoi_id, md_sanpham_id, soluong, macdinh, ngaytao,ngaycapnhat,hoatdong)
select 
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(IDDongGoi as nvarchar(32)))),32)),
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(IDDongGoi as nvarchar(32)))),32)),
	(select RIGHT(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(MaHH as nvarchar(32)))),32)),
	SlInner, 1, GETDATE(), GETDATE(), 1
from ancodb.dbo.dmCachDongGoi






create table New_Table
(
	ngaydonhang nvarchar(100)
	,Ngaygiaohang nvarchar(100)
	,madtkd nvarchar(100)
	,sanpham nvarchar(100)
	,giasanpham nvarchar(100)
	,tokhai nvarchar(100)
	,solo nvarchar(100)
	,solong nvarchar(100)
	,cdfeet nvarchar(100)
	,cvfeet nvarchar(100)
	,cvinc nvarchar(100)
	,docno nvarchar(100)
)
BULK INSERT New_Table
FROM 'c:\importPO_basic.csv'  --or a .csv file
WITH
(
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
)
GO



select
	dgsp.md_donggoisanpham_id, dg.ten_donggoi, sp.ma_sanpham
	, dg.sl_inner, (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_inner) as dvtinner
	, dg.l1, dg.w1, dg.h1
	, dg.sl_outer, (select ten_dvt from md_donvitinh where md_donvitinh_id = dg.dvt_outer) as dvtouter
	, dg.l2_mix, dg.w2_mix, dg.h2_mix, dg.v2
	, dg.sl_cont_mix
	, dg.vd, dg.vn, dg.vl, dg.ghichu_vachngan
	, dgsp.soluong, dgsp.macdinh 
from 
	md_sanpham sp, md_donggoisanpham dgsp, md_donggoi dg
where
	sp.md_sanpham_id = dgsp.md_sanpham_id
	AND dgsp.md_donggoi_id = dg.md_donggoi_id
	



select 
	pbg.ten_phienbangia, sp.ma_sanpham, gsp.gia, dt.ma_iso, pbg.ngay_hieuluc
from
	md_sanpham sp, md_banggia bg, md_phienbangia pbg, md_giasanpham gsp, md_dongtien dt
where
	gsp.md_phienbangia_id = pbg.md_phienbangia_id
	AND pbg.md_banggia_id  = bg.md_banggia_id
	AND bg.md_dongtien_id = dt.md_dongtien_id
	AND gsp.hoatdong = 1
	AND bg.banggiaban = 1
	AND sp.md_sanpham_id = N'812d4abe6415d09ca2846c470347e5fe'
	AND gsp.md_sanpham_id = N'812d4abe6415d09ca2846c470347e5fe'
	



SELECT COUNT(*) AS count 
FROM md_sanpham sp, md_banggia bg, md_phienbangia pbg, md_giasanpham gsp, md_dongtien dt  
WHERE 
	sp.md_sanpham_id = gsp.md_giasanpham_id  
	AND gsp.md_phienbangia_id = pbg.md_phienbangia_id  
	AND pbg.md_banggia_id  = bg.md_banggia_id  
	AND bg.md_dongtien_id = dt.md_dongtien_id  
	AND gsp.hoatdong = 1 
	AND bg.banggiaban = 1 
	AND sp.md_sanpham_id = N'812d4abe6415d09ca2846c470347e5fe' 
	AND gsp.md_sanpham_id = N'812d4abe6415d09ca2846c470347e5fe'



select top(1)
	sp.ma_sanpham, gsp.gia, dt.ma_iso
from
	md_sanpham sp, md_banggia bg, md_phienbangia pbg, md_giasanpham gsp, md_dongtien dt
where
	sp.md_sanpham_id = gsp.md_sanpham_id
	AND pbg.md_phienbangia_id = gsp.md_phienbangia_id
	AND pbg.md_banggia_id = bg.md_banggia_id
	AND bg.md_dongtien_id = dt.md_dongtien_id
	AND gsp.hoatdong = 1
	AND sp.md_sanpham_id = N'34c45b7c606e534aead55d67470265fb'
order by pbg.ngay_hieuluc desc









select 
	dh.c_donhang_id, dh.sochungtu, lct.tenchungtu,  tthai.tentrangthai
	, dtkd.ten_dtkd, dh.sohd, dh.ngaylap, dh.nguoilap, dh.soorder
	, dh.port_loading, dh.discount, dh.shipmentdate, dh.shipmenttime
	, pmt.ten_paymentterm,  dgoi.ten_donggoi as tendonggoi
	, (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dh.trongluong_id) as trongluong
	, dtien.ma_iso as tendongtien
	, (select ten_dvt from md_donvitinh dvt where dvt.md_donvitinh_id = dh.kichthuoc_id) as kichthuoc
	, bgia.ten_banggia as tenbanggia ,  ROW_NUMBER() OVER (ORDER BY c_donhang_id desc) as RowNum  
FROM 
	c_donhang dh, md_loaichungtu lct, md_trangthai tthai, md_doitackinhdoanh dtkd, md_cangbien cbien
	,  md_paymentterm pmt, md_donggoi dgoi, md_dongtien dtien, md_banggia bgia 
WHERE 
	dh.md_loaichungtu_id = lct.md_loaichungtu_id  
	AND dh.md_trangthai_id = tthai.md_trangthai_id  
	AND dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id  
	AND dh.port_loading = cbien.md_cangbien_id  
	AND dh.md_paymentterm_id = pmt.md_paymentterm_id  
	AND dh.md_donggoi_id = dgoi.md_donggoi_id  
	AND dh.md_dongtien_id = dtien.md_dongtien_id  
	AND dh.md_banggia_id = bgia.md_banggia_id
	
	
	
	
	
select 
	ddh.c_dongdonhang_id, dg.ten_donggoi, sp.md_sanpham_id
	, sp.ma_sanpham,  dtkd.ten_dtkd, kho.ten_kho, ddh.line
	, ddh.soluong_po,  ddh.soluong_dathang, ddh.soluong_dagiao
	, ddh.soluong_conlai,  ddh.gia, dg.ten_donggoi, ddh.sl_inner
	, ddh.l1, ddh.h1, ddh.w1,  ddh.sl_outer, ddh.l2, ddh.h2, ddh.w2
	, ddh.v2, ddh.slcont,  ddh.tongcbm, ddh.ngaytao, ddh.nguoitao
	, ddh.ngaycapnhat,  ddh.nguoicapnhat, ddh.mota, ddh.hoatdong
	,   ROW_NUMBER() OVER (ORDER BY c_dongdonhang_id desc) as RowNum  
FROM 
	c_dongdonhang ddh, c_donhang dh, md_sanpham sp
	,  md_doitackinhdoanh dtkd, md_kho kho, md_donggoi dg  
WHERE 
	ddh.c_donhang_id = dh.c_donhang_id  
	AND ddh.md_sanpham_id = sp.md_sanpham_id  
	AND ddh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id  
	AND ddh.md_kho_id = kho.md_kho_id  
	AND ddh.md_donggoi_id = dg.md_donggoi_id




select * from c_danhsachdathang
select * from c_dongdonhang

select
	ddsdh.c_dongdsdh_id , dsdh.sochungtu
	, ddh.line, sp.ma_sanpham , ddsdh.mota_tiengviet
	, (select md_doitackinhdoanh_id from md_doitackinhdoanh dtkd where dtkd.md_doitackinhdoanh_id = ddsdh.khachhang_id) as kh
	, sl_cannhap,gianhap
	, (select md_doitackinhdoanh_id from md_doitackinhdoanh dtkd where dtkd.md_doitackinhdoanh_id = ddsdh.nhacungung_id) as ncc
	, md_banggiancu_id,huongdan_dathang
	, han_giaohang,tem_dan, ddsdh.line

from
	c_dongdsdh ddsdh, c_danhsachdathang dsdh
	, c_dongdonhang ddh, md_sanpham sp
	, md_banggia bg
where
	ddsdh.c_danhsachdathang_id = dsdh.c_danhsachdathang_id
	AND ddsdh.c_dongdonhang_id = ddh.c_dongdonhang_id
	AND ddsdh.md_sanpham_id = sp.md_sanpham_id
	AND ddsdh.md_banggiancu_id = bg.md_banggia_id




select
	tc.c_thuchi_id, tc.sophieu,
	dtkd.ten_dtkd, tc.nguoi_giaonop,
	tc.ngay_giaonop, tc.sotien,
	dt.ma_iso, tc.tygia,
	tc.lydo, tc.loaiphieu,
	tc.sochungtu, tc.quydoi_vnd,
	tc.tk_no, tc.tk_co,
	tc.tk_quy, tc.so_dadinhkhoan,
	tc.so_chuadinhkhoan, tc.so_dachiphi,
	tt.tentrangthai, lct.tenchungtu
from
	c_thuchi tc, md_doitackinhdoanh dtkd, md_dongtien dt, md_trangthai tt, md_loaichungtu lct
where
	tc.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	AND tc.md_dongtien_id = dt.md_dongtien_id
	AND tc.md_trangthai_id = tt.md_trangthai_id
	AND tc.md_loaichungtu_id = lct.md_loaichungtu_id
	
	


select * from c_dongpklinv

select
	dpk.c_dongpklinv_id, pk.so_pkl,
	dnx.line, sp.md_sanpham_id,
	sp.ma_sanpham, dpk.mota_tienganh,
	dpk.soluong, dpk.gia,
	dpk.thanhtien, dpk.line,
	dpk.ngaytao, dpk.nguoitao,
	dpk.ngaycapnhat, dpk.nguoicapnhat,
	dpk.mota, dpk.hoatdong
from
	c_dongpklinv dpk, c_packinginvoice pk, c_dongnhapxuat dnx, md_sanpham sp
where
	dpk.c_packinginvoice_id = pk.c_packinginvoice_id
	AND dpk.c_dongnhapxuat_id = dnx.c_dongnhapxuat_id
	AND dpk.md_sanpham_id = sp.md_sanpham_id



select * from md_doitackinhdoanh where md_doitackinhdoanh_id = '020dbf4af6b862fa07cc13ab300cb384'
select * from c_baogia
 select baogia.c_baogia_id, baogia.sobaogia, dtkd.ten_dtkd, pmt.ten_paymentterm, baogia.shipmentdate,
             banggia.ten_banggia, ngaybaogia, ngayhethan, trongluong, cb.ten_cangbien,
             baogia.totalcbm, baogia.totalcbf, baogia.totalquo, dt.ma_iso, tt.tentrangthai, 
             baogia.ngaytao, baogia.nguoitao, baogia.ngaycapnhat, baogia.nguoicapnhat, baogia.mota, baogia.hoatdong
             FROM c_baogia baogia, md_doitackinhdoanh dtkd, md_banggia banggia, md_cangbien cb, md_dongtien dt, md_trangthai tt, md_paymentterm pmt
             WHERE baogia.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id 
             AND baogia.md_paymentterm_id = pmt.md_paymentterm_id
             AND baogia.md_banggia_id = banggia.md_banggia_id 
             AND baogia.md_cangbien_id = cb.md_cangbien_id
             AND baogia.md_trangthai_id = tt.md_trangthai_id
             AND banggia.md_dongtien_id = dt.md_dongtien_id


SELECT TOP (1) gsp.gia
FROM 
	c_baogia baogia, md_banggia banggia
	, md_phienbangia pbg, md_giasanpham gsp
WHERE
	baogia.md_banggia_id = banggia.md_banggia_id
	AND banggia.md_banggia_id = pbg.md_banggia_id
	AND pbg.md_phienbangia_id = gsp.md_phienbangia_id
	AND baogia.c_baogia_id = N'b893605f109869934a038dd748e41d81'
	AND gsp.md_sanpham_id = N'01d662314b55f295eeedd5f5620d7815'
	AND pbg.hoatdong = 1
	AND pbg.ngay_hieuluc < baogia.ngaybaogia
ORDER BY pbg.ngay_hieuluc DESC




select
	dg.md_donggoi_id, dg.sl_inner
	, dg.dvt_inner
	, dg.l1, dg.w1, dg.h1
	, dg.sl_outer
	, dg.dvt_outer
	, dg.l2_mix, dg.w2_mix, dg.h2_mix, dg.v2, dg.soluonggoi_ctn
	, dg.vd, dg.vn, dg.vl, dg.ghichu_vachngan
from
	md_donggoi dg, md_donggoisanpham dgsp
where
	dg.md_donggoi_id = dgsp.md_donggoi_id
	AND dgsp.macdinh = 1
	AND dgsp.md_sanpham_id = N'01d662314b55f295eeedd5f5620d7815'
	
	




SELECT TOP (1) gsp.gia
FROM 
c_baogia baogia
, md_banggia banggia 
, md_phienbangia pbg 
, md_giasanpham gsp
WHERE 
baogia.md_banggia_id = banggia.md_banggia_id 
AND banggia.md_banggia_id = pbg.md_banggia_id 
AND pbg.md_phienbangia_id = gsp.md_phienbangia_id 
AND baogia.c_baogia_id = N'1c0bdd9e27759695417e60b328ff5973'
AND gsp.md_sanpham_id = N'01d662314b55f295eeedd5f5620d7815'
AND pbg.ngay_hieuluc < baogia.ngaybaogia
ORDER BY pbg.ngay_hieuluc DESC


SELECT  
	gsp.md_sanpham_id, gsp.gia, ngay_hieuluc, ngaybaogia
FROM
	c_baogia baogia
	, md_banggia banggia
	, md_phienbangia pbg
	, md_giasanpham gsp
WHERE
	baogia.md_banggia_id = banggia.md_banggia_id
	--AND banggia.md_banggia_id = pbg.md_banggia_id
	AND pbg.md_phienbangia_id = gsp.md_phienbangia_id
	AND baogia.c_baogia_id = 'e35e84188ae3524dd965052181712ecd'
	AND gsp.md_sanpham_id = '94b2d93c086f4fc65b5e65916b52a9ef'
	AND pbg.ngay_hieuluc <= baogia.ngaybaogia
ORDER BY pbg.ngay_hieuluc DESC






SELECT COUNT(*) AS count  
FROM 
	c_donhang dh, md_loaichungtu lct
	, md_doitackinhdoanh dtkd
	,  md_cangbien cbien, md_paymentterm pmt
	, md_donggoi dgoi, md_dongtien dtien, md_banggia bgia 
WHERE 
	dh.md_loaichungtu_id = lct.md_loaichungtu_id   
	AND dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id  
	AND dh.port_loading = cbien.md_cangbien_id  
	AND dh.md_paymentterm_id = pmt.md_paymentterm_id  
	AND dh.md_donggoi_id = dgoi.md_donggoi_id  
	AND dh.md_dongtien_id = dtien.md_dongtien_id  
	AND dh.md_banggia_id = bgia.md_banggia_id
	
	

select
	*
from 
	c_donhang dh, md_doitackinhdoanh dtkd, md_cangbien cb
	, md_paymentterm pmt, md_trongluong tl, md_kichthuoc kt
	, md_banggia bg, md_dongtien dt
where
	dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	AND dh.md_cangbien_id = cb.md_cangbien_id
	AND dh.md_paymentterm_id = pmt.md_paymentterm_id
	AND dh.md_trongluong_id = tl.md_trongluong_id
	AND dh.md_kichthuoc_id = kt.md_kichthuoc_id
	AND dh.md_banggia_id = bg.md_banggia_id
	AND dh.md_dongtien_id = dt.md_dongtien_id









INSERT INTO [esc_anco].[dbo].[c_donhang]
           ([c_donhang_id],[c_baogia_id],[sochungtu]
           ,[md_doitackinhdoanh_id],[ngaylap],[nguoilap]
           ,[md_cangbien_id],[discount],[shipmentdate]
           ,[shipmenttime] ,[md_paymentterm_id],[md_trongluong_id]
           ,[md_kichthuoc_id],[payer],[portdischarge]
           ,[amount],[totalcbm],[totalcbf],[ismakepi]
           ,[md_banggia_id],[md_trangthai_id],[md_dongtien_id]
           ,[ngaytao] ,[ngaycapnhat],[hoatdong])

SELECT [c_baogia_id] ,[sobaogia] ,[md_doitackinhdoanh_id]
      ,[md_paymentterm_id],[shipmenttime],[md_banggia_id]
      ,[ngaybaogia],[ngayhethan],[md_trongluong_id]
      ,[md_cangbien_id] ,[totalcbm] ,[totalcbf]
      ,[totalquo],[md_trangthai_id] ,[md_dongtien_id]
  FROM [esc_anco].[dbo].[c_baogia]
GO






INSERT INTO [c_dongdonhang]
           ([c_dongdonhang_id],[c_donhang_id],[md_sanpham_id]
           ,[ma_sanpham_khach],[sothutu],[giafob]
           ,[soluong],[sl_inner],[l1],[w1]
           ,[h1],[sl_outer],[l2]
           ,[w2],[h2],[v2]
           ,[sl_cont],[vd],[vn],[vl]
           ,[ghichu_vachngan]
           ,[ngaytao],[ngaycapnhat],[hoatdong])
SELECT [c_chitietbaogia_id],[c_baogia_id],[md_sanpham_id]
      ,[ma_sanpham_khach],[sothutu],[giafob]
      ,[soluong],[sl_inner],[l1],[w1]
      ,[h1],[sl_outer],[l2]
      ,[w2],[h2],[v2]
      ,[sl_cont],[vd],[vn],[vl]
      ,[ghichu_vachngan]
      , GETDATE(), GETDATE(), 1
  FROM [c_chitietbaogia]
GO


INSERT INTO [esc_anco].[dbo].[c_tuannangluc]
           ([c_tuannangluc_id]
           ,[md_doitackinhdoanh_id]
           ,[ten_tuan]
           ,[tuanthu]
           ,[ngaybatdau]
           ,[nam]
           ,[ngaytao]
           ,[ngaycapnhat]
           ,[hoatdong])
select
	c_tuannl_id, md_doitackinhdoanh_id, ten, tuanthu, ngatbatdau, nam, GETDATE(), GETDATE(), 1
from c_tuannl
GO






select * from c_danhsachdathang

	select 
		* 
	from 
		c_danhsachdathang dsdh, c_donhang dh
	where 
		dsdh.c_donhang_id = dh.c_donhang_id


select * from c_donhang where c_donhang_id = '5cd642ec6509ff6a74de2b985014c17c'



 select 
	dsdh.c_danhsachdathang_id, dsdh.sochungtu sodsdh
	, dsdh.ngaylap,  dsdh.hangiaohang_po, dsdh.nguoi_phutrach
	, dsdh.nguoi_dathang,  dh.sochungtu as sodh
	, dsdh.so_po, dsdh.md_trangthai_id
	, dsdh.ngaytao,  dsdh.nguoitao, dsdh.ngaycapnhat, dsdh.nguoicapnhat, dsdh.mota, dsdh.hoatdong
	,  ROW_NUMBER() OVER (ORDER BY c_danhsachdathang_id desc) as RowNum  
FROM c_danhsachdathang dsdh, c_donhang dh
WHERE dsdh.c_donhang_id = dh.c_donhang_id



select * from md_cangxuathang

select cxh.md_cangxuathang_id, sp.md_sanpham_id, sp.ma_sanpham, cb.ten_cangbien, cxh.macdinh,  cxh.ngaytao, cxh.nguoitao, cxh.ngaycapnhat, cxh.nguoicapnhat, cxh.mota, cxh.hoatdong  ,  ROW_NUMBER() OVER (ORDER BY cxh.md_cangxuathang_id desc) as RowNum  FROM md_cangxuathang cxh, md_sanpham sp, md_cangbien cb  WHERE cxh.md_sanpham_id = sp.md_sanpham_id  AND cxh.md_cangbien_id = cb.md_cangbien_id


select * from c_danhsachdathang
select * from c_dongdsdh

select * from md_loaisanpham
select * from c_donhang



select 
	dsdh.sochungtu, dsdh.hangiaohang_po,
	sp.ma_sanpham
from 
	c_danhsachdathang dsdh
	, c_dongdsdh ddsdh
	, md_sanpham sp
where
	dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
	AND ddsdh.md_sanpham_id = sp.md_sanpham_id
order by dsdh.sochungtu
	

select * from c_donhang
select * from c_dongdonhang
select * from c_danhsachdathang
select * from c_dongdsdh

select 
	dh.sochungtu as ct_dh
	, dsdh.sochungtu as ct_dathang
	, ddsdh.mota_tienganh as ta
	, ddsdh.ma_sanpham_khach as spk
	, ddsdh.gianhap as gianhap
	, ddsdh.han_giaohang as han_giaohang
	, ddsdh.sl_dathang as sldathang
	, ddsdh.sl_dagiao as sldagiao
	, ddsdh.sl_conlai as slconlai
from 
	c_danhsachdathang dsdh, c_donhang dh, c_dongdsdh ddsdh
where 
	dsdh.c_donhang_id = dh.c_donhang_id
	AND dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
	AND dh.hoatdong = 1
	select * from c_dongnhapxuat
	

select * from c_dongnhapxuat

select * from c_xacnhan_xuatkho

select * from c_nhapxuat
select * from md_sochungtu
select * from md_loaichungtu

select 
	dh.c_donhang_id, dh.sochungtu
from 
	c_donhang dh, c_nhapxuat nx
where 
	dh.c_donhang_id = nx.c_donhang_id
	AND nx.md_loaichungtu_id = 'XK'
	AND nx.hoatdong = 1



/* report quotation */
select  from md_sanpham
select * from md_anhsanpham where url like '%51-00100%'
select 
    dtkd.ten_dtkd as ten_dtkd, bg.ngaybaogia as ngay_quo, dtkd.diachi as diachi,
    SUBSTRING(ma_sanpham,0,9) as url,
    sp.ma_sanpham as ma_sanpham, ctbg.ma_sanpham_khach as ma_spkhach,
    ctbg.mota_tienganh as mota_ta, ctbg.trongluong, ctbg.l1, ctbg.w1, ctbg.h1,
    ctbg.l2, ctbg.w2, ctbg.h2, ctbg.sl_cont, ctbg.giafob, cb.ten_cangbien as cangbien,
    bg.mota as ghichu, CAST( bg.shipmenttime - bg.ngaybaogia as int) as smtime, pmt.mota as dieukhoan,
    bg.ngayhethan
from 
    c_baogia bg, c_chitietbaogia ctbg, md_sanpham sp
    , md_doitackinhdoanh dtkd, md_donggoi dg
    , md_cangbien cb, md_paymentterm pmt
where
    bg.c_baogia_id = ctbg.c_baogia_id
    AND bg.md_paymentterm_id = pmt.md_paymentterm_id
    AND bg.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
    AND ctbg.md_sanpham_id = sp.md_sanpham_id
    AND ctbg.md_donggoi_id = dg.md_donggoi_id
    AND ctbg.md_cangbien_id = cb.md_cangbien_id 
/* ## report quotation */


select * from c_donhang
select * from c_danhsachdathang
select * from c_dongdsdh
select * from c_dongdonhang

select 
	dtkd.ten_dtkd, dsdh.sochungtu, sp.ma_sanpham, ddsdh.mota_tienganh, dvt.ma_edi, dg.ten_donggoi, ddsdh.sl_conlai
from 
	c_danhsachdathang dsdh, c_dongdsdh ddsdh,
	md_doitackinhdoanh dtkd, md_sanpham sp, md_donvitinhsanpham dvt, md_donggoi dg
where 
	dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
	AND dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	AND ddsdh.md_sanpham_id = sp.md_sanpham_id
	AND dsdh.c_donhang_id = N'34C12A6853214D0A92B1A6F63CCE54F8'
	
	
	
	
select 
    dtkd.ten_dtkd, dsdh.sochungtu, sp.ma_sanpham, ddsdh.mota_tienganh
    , sp.md_donvitinhsanpham_id, ddsdh.md_donggoi_id, ddsdh.sl_conlai
from 
    c_danhsachdathang dsdh, c_dongdsdh ddsdh,
    md_doitackinhdoanh dtkd, md_sanpham sp
where 
    dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
    AND dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
    AND ddsdh.md_sanpham_id = sp.md_sanpham_id
    AND dsdh.c_donhang_id = '25FA0442932749E1A5536F35A655D7C5'
    
    
select 
	dh.c_donhang_id
	, dsdh.c_danhsachdathang_id
	, dh.sochungtu as ct_dh
	, dsdh.sochungtu as ct_dsdh
	, dtkd.ten_dtkd
	, dh.ngaylap 
from 
	c_donhang dh, c_danhsachdathang dsdh, md_doitackinhdoanh dtkd
where
	dh.c_donhang_id = dsdh.c_donhang_id
	AND dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	
	
select 
    sp.ma_sanpham, ddsdh.mota_tienganh
    , sp.md_donvitinhsanpham_id, ddsdh.md_donggoi_id, ddsdh.sl_conlai
from 
    c_dongdsdh ddsdh,
    md_doitackinhdoanh dtkd, md_sanpham sp
where 
    ddsdh.md_sanpham_id = sp.md_sanpham_id
    


select * from c_nhapxuat
select 
	nx.c_nhapxuat_id, nx.sophieu, nx.ngay_giaonhan, dh.sochungtu, nx.socontainer, nx.soseal
	, sp.ma_sanpham, dnx.mota_tiengviet, 
	, dnx.sl_dathang, dnx.dongia, (dnx.slphai_nhapxuat * dnx.dongia) as thanhtien
from 
	c_nhapxuat nx , c_dongnhapxuat dnx, c_donhang dh, md_sanpham sp, md_donvitinhsanpham dvt
where
	nx.c_nhapxuat_id = dnx.c_nhapxuat_id
	AND nx.c_donhang_id = dh.c_donhang_id
	AND dnx.md_sanpham_id = sp.md_sanpham_id
	AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
	
	
	
	SELECT 
	            nx.sophieu, nx.ngay_giaonhan as ngay, dh.sochungtu, nx.socontainer as so_con, nx.soseal as seal
                , sp.ma_sanpham, sp.mota_tiengviet, dvt.ma_edi as dvt
	            , dnx.sl_dathang as soluong, dnx.dongia as gia, (dnx.sl_dathang * dnx.dongia) as thanhtien
            FROM 
	            c_nhapxuat nx , c_dongnhapxuat dnx
                , c_donhang dh, md_sanpham sp, md_donvitinhsanpham dvt
            WHERE
	            nx.c_nhapxuat_id = dnx.c_nhapxuat_id
	            AND nx.c_donhang_id = dh.c_donhang_id
	            AND dnx.md_sanpham_id = sp.md_sanpham_id
	            AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
	            

select * from c_danhsachdathang
select * from c_donhang
select * from c_dongdsdh


select 
	dsdh.sochungtu, dsdh.so_po, dtkd.ten_dtkd, dtkd.tel, dtkd.fax
	, sp.ma_sanpham, ddsdh.ma_sanpham_khach, ddsdh.mota_tienganh, dvt.ma_edi, ddsdh.sl_dathang, ddsdh.gianhap
	,( ddsdh.sl_dathang * ddsdh.gianhap) as thanhtien, ddsdh.han_giaohang, ddsdh.huongdan_dathang
from 
	c_danhsachdathang dsdh, md_doitackinhdoanh dtkd, md_sanpham sp
	, c_dongdsdh ddsdh, md_donvitinhsanpham dvt
where
	dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	AND dsdh.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
	AND ddsdh.md_sanpham_id = sp.md_sanpham_id
	AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id



SELECT COUNT(*) AS count  FROM c_kehoachxuathang kh, c_donhang dh, md_doitackinhdoanh dtkd  WHERE kh.c_donhang_id = dh.c_donhang_id  AND kh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id  AND kh.shipmenttime >= N'9/1/2012 12:00:00 AM' AND kh.shipmenttime <= N'9/30/2012 12:00:00 AM'

SELECT 
    nx.sophieu, dtkd.ma_dtkd, dtkd.ten_dtkd, nx.ngay_giaonhan, nx.ngay_giaonhan as ngay, dh.sochungtu
    , nx.socontainer as so_con, nx.soseal as seal
    , sp.ma_sanpham, sp.mota_tiengviet, dvt.ma_edi as dvt
    , dnx.sl_dathang as soluong, dnx.dongia as dongia, (dnx.sl_dathang * dnx.dongia) as thanhtien
FROM 
    c_nhapxuat nx , c_dongnhapxuat dnx, md_doitackinhdoanh dtkd
    , c_donhang dh, md_sanpham sp, md_donvitinhsanpham dvt
WHERE
    nx.c_nhapxuat_id = dnx.c_nhapxuat_id
    AND nx.c_donhang_id = dh.c_donhang_id
    AND nx.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
    AND dnx.md_sanpham_id = sp.md_sanpham_id
    AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
    



select * from nhanvien
select SUBSTRING(filter,0,3) from md_color_reference


select color.mau, color.url from c_baogia bg, md_color_reference color where bg.c_baogia_id = color.c_baogia_id and bg.c_baogia_id =''



select * from c_baogia
select * from c_chitietbaogia
select * from md_doitackinhdoanh


select 
	dtkd.ten_dtkd, dtkd.diachi, bg.ngaybaogia
	,sp.ma_sanpham, SUBSTRING(sp.ma_sanpham,0,9) as pic, sp.ma_sanpham, ctbg.ma_sanpham_khach
	, ctbg.mota_tienganh, ctbg.trongluong, ctbg.sl_inner
	, dg.sl_inner, dg.ten_donggoi, ctbg.l1, ctbg.w1, ctbg.h1, ctbg.l2, ctbg.w2, ctbg.h2
	, (ctbg.l2 * ctbg.h2 * ctbg.w2/1000000 * ctbg.soluong/ctbg.sl_outer) as cbm
	, (ctbg.soluong / ctbg.sl_outer) as nopack, ctbg.giafob
	, sp.l_cm, sp.w_cm, sp.h_cm
	, bg.shipmenttime, cb.ten_cangbien, pmt.mota
	, bg.ngayhethan
	
from 
	c_baogia bg, c_chitietbaogia ctbg
	, md_doitackinhdoanh dtkd, md_sanpham sp
	, md_donggoi dg, md_cangbien cb
	, md_paymentterm pmt
where
	bg.c_baogia_id = ctbg.c_baogia_id
	AND bg.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
	AND ctbg.md_sanpham_id = sp.md_sanpham_id
	AND ctbg.md_donggoi_id = dg.md_donggoi_id
	AND bg.md_cangbien_id = cb.md_cangbien_id
	AND bg.md_paymentterm_id = pmt.md_paymentterm_id
	


select 
	SUBSTRING(color.filter), color.mau
from 
	c_baogia bg, md_color_reference color 
where 
	bg.c_baogia_id = color.c_baogia_id
	
	
	
select * from c_dongdonhang
	
select 
    dh.sochungtu, dtkd.ten_dtkd, dh.payer, dh.ngaylap 
    , sp.ma_sanpham, ddh.ma_sanpham_khach, ddh.mota_tienganh
    , ddh.sl_outer, dg.ten_donggoi, ddh.soluong, dvt.ten_dvt, ddh.giafob
    , sp.l_cm, sp.w_cm, sp.h_cm ,dh.shipmenttime, cb.ten_cangbien, pmt.mota, ddh.v2, ddh.sl_outer, sp.trongluong
    , ddh.sl_outer
from
    c_donhang dh, md_doitackinhdoanh dtkd
    , c_dongdonhang ddh, md_sanpham sp, md_donggoi dg
    , md_donvitinhsanpham dvt, md_cangbien cb, md_paymentterm pmt
where
    dh.c_donhang_id = ddh.c_donhang_id
    AND dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
    AND ddh.md_sanpham_id = sp.md_sanpham_id
    AND ddh.md_donggoi_id = dg.md_donggoi_id
    AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
    AND dh.md_paymentterm_id = pmt.md_paymentterm_id
    AND dh.md_cangbien_id = cb.md_cangbien_id



select dbo.get_commodity('7ad4eee2c3f0f8ecc21f26569abe3ca8')

select 
    dh.sochungtu, dtkd.ten_dtkd, dh.payer, dh.ngaylap 
    , sp.ma_sanpham, SUBSTRING(sp.ma_sanpham,0,9) + '.jpg' as url, ddh.ma_sanpham_khach, ddh.mota_tienganh
    , ddh.sl_outer, dg.ten_donggoi, ddh.soluong, dvt.ten_dvt as dvt_sp, ddh.giafob
    , sp.l_cm, sp.w_cm, sp.h_cm, dh.shipmenttime, cb.ten_cangbien, pmt.mota
    , ddh.v2, ddh.sl_outer, (sp.trongluong * ddh.soluong) as trluong
    , (ddh.soluong * ddh.giafob) as amount, (ddh.soluong * ddh.sl_outer) as ofpack
    , ( cast(dh.sl_cont as nvarchar) + ' ' + dh.loai_cont) as no_cont
    , dh.portdischarge
from
    c_donhang dh, md_doitackinhdoanh dtkd
    , c_dongdonhang ddh, md_sanpham sp, md_donggoi dg
    , md_donvitinhsanpham dvt, md_cangbien cb, md_paymentterm pmt
where
    dh.c_donhang_id = ddh.c_donhang_id
    AND dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
    AND ddh.md_sanpham_id = sp.md_sanpham_id
    AND ddh.md_donggoi_id = dg.md_donggoi_id
    AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
    AND dh.md_paymentterm_id = pmt.md_paymentterm_id
    AND dh.md_cangbien_id = cb.md_cangbien_id
    
    
select md_donggoi_id, ngaytao from c_dongdonhang order by ngaytao desc


select portdischarge from c_donhang
select * from c_dongdsdh



select 
    sp.ma_sanpham, ddsdh.mota_tienganh, ddsdh.ma_sanpham_khach
    , dvt.ten_dvt, dg.ten_donggoi, ddsdh.sl_dathang
from 
    c_dongdsdh ddsdh, md_doitackinhdoanh dtkd, md_sanpham sp, md_donvitinhsanpham dvt
    , md_donggoi dg
where 
    ddsdh.md_sanpham_id = sp.md_sanpham_id
    AND sp.md_donvitinhsanpham_id = dvt.md_donvitinhsanpham_id
    AND ddsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
    AND ddsdh.md_donggoi_id = dg.md_donggoi_id
    AND ddsdh.c_danhsachdathang_id = N'{0}'
    


select * from c_kehoachxuathang


select * from c_dongnhapxuat where c_dongnhapxuat_id = '5986d3907972204328ade3976a4cd341'

select distinct ctn.manv from md_chitietnhom ctn 
                                      where ctn.md_nhom_id IN (select md_nhom_id from md_chitietnhom where manv = 'ancoadmin' and nguoiquanly = 1)