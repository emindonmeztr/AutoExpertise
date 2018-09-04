CREATE TABLE [dbo].[processesbase]
(
	 expertiz_id INT NOT NULL PRIMARY KEY,
	 sase_no INT NOT NULL ,
	 musteri_id INT NOT NULL,
	 tarih DATE,
	 plaka VARCHAR(50),
	 marka VARCHAR(128),
	 model VARCHAR(128),
	 vites_tipi VARCHAR(50),
	 renk int,
	 yakit_tipi int,
	 model_yili int,
	 motor_hacmi int,
	 motor_gucu int,
	 km int,
	 aracsahibi VARCHAR(128),
	 aracsahibi_tel VARCHAR(128)
	  
)
