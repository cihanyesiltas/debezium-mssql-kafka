-- Create the test database
CREATE DATABASE testDB;
GO
USE testDB;
EXEC sys.sp_cdc_enable_db;

CREATE TABLE jobs (
  id INTEGER IDENTITY(101,1) NOT NULL PRIMARY KEY,
  title VARCHAR(255) NOT NULL,
  description VARCHAR(512),
  ip VARCHAR(255),
  taxNumber VARCHAR(255)
);

INSERT INTO testDB.dbo.jobs (title, description, ip, taxNumber) VALUES (N'Garson', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam pellentesque tempor magna aliquet pretium. Proin maximus purus ut rhoncus vehicula. Nam ultricies nisl eu arcu.', '101.0.0.0','101000000');
INSERT INTO testDB.dbo.jobs (title, description, ip, taxNumber) VALUES (N'Asçi', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce a eros non neque viverra eleifend. Phasellus eu pharetra nisl, non convallis magna. Vivamus sed accumsan.', '102.0.0.0','102000000');
INSERT INTO testDB.dbo.jobs (title, description, ip, taxNumber) VALUES (N'Güvenlik Görevlisi', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed convallis, erat a elementum rutrum, erat dolor pellentesque ipsum, ac tristique elit lacus vehicula nibh. Sed.', '103.0.0.0','103000000');
INSERT INTO testDB.dbo.jobs (title, description, ip, taxNumber) VALUES (N'Garaj Amiri', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent iaculis lacus tempor nibh sagittis, a interdum diam placerat. Mauris eleifend ipsum eget turpis interdum, id.', '104.0.0.0','104000000');
INSERT INTO testDB.dbo.jobs (title, description, ip, taxNumber) VALUES (N'Temizlik Görevlisi', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent ac risus sapien. Nunc et ligula eu nisl vehicula pellentesque vitae pretium tortor. Nam euismod sollicitudin.', '105.0.0.0','105000000');



Declare @Id int
Set @Id = 1

While @Id <= 100000
Begin
   Insert Into dbo.jobs (title, description, ip, taxNumber)
    values ('Title - ' + CAST(@Id as nvarchar(10)),'Description - ' + CAST(@Id as nvarchar(10)), '1.0.0.1', CAST(@Id as nvarchar(10)))
   Print @Id
   Set @Id = @Id + 1
End


EXEC sys.sp_cdc_enable_table @source_schema = 'dbo', @source_name = 'jobs', @role_name = NULL, @supports_net_changes = 0;


