USE [NGANHANG]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetColumn]    Script Date: 11/05/2022 4:22:33 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetColumn]
	-- khai bao cac bien tam 
	@OBJECT_ID int
AS
BEGIN
	DECLARE @primary_key nvarchar(10);
	SELECT @primary_key = c.name
		FROM NGANHANG.sys.indexes i
			inner join NGANHANG.sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
			inner join NGANHANG.sys.columns c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
		WHERE i.is_primary_key = 1
		AND i.object_ID = @OBJECT_ID


	SELECT a.name, b.DATA_TYPE, b.CHARACTER_MAXIMUM_LENGTH, IIF(@primary_key = a.name, 1, 0 ) as is_primary_key
	FROM 
		NGANHANG.sys.columns a
	JOIN NGANHANG.INFORMATION_SCHEMA.COLUMNS b
	ON a.name = b.COLUMN_NAME AND b.TABLE_NAME = OBJECT_NAME(@OBJECT_ID)
	WHERE a.object_id = @OBJECT_ID AND a.is_rowguidcol = 0 
	GROUP BY a.name, b.DATA_TYPE, b.CHARACTER_MAXIMUM_LENGTH

END
