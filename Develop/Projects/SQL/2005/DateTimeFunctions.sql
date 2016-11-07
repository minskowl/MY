/****** Object:  UserDefinedFunction [dbo].[dateTrim]    Script Date: 05/18/2009 12:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[dateTrim] (@date datetime)
returns datetime
as
begin
-- convert datetime to date
return convert( datetime , convert(varchar(8),@date, 112),112);
end
GO
CREATE function [dbo].[timeTrim] (@date datetime)
returns datetime
as
begin
-- convert datetime to date
return CONVERT(varchar(8), @date, 108);
end
GO
CREATE FUNCTION [dbo].[currentDate]() 
RETURNS datetime
AS
BEGIN
return convert( datetime , convert(varchar(8),GETDATE(), 112),112);
END
GO
CREATE FUNCTION [dbo].[addDateSpan] 
	(
	
	@Date DATETIME,
	@DatePart NVARCHAR(10),
	@Value INT
	)
RETURNS DATETIME
AS
	BEGIN
	RETURN CASE LOWER(@DatePart) 
	             WHEN 'day' THEN DATEADD(day,@Value,@Date)
	             WHEN 'month' THEN DATEADD(month,@Value,@Date)
	             WHEN 'week' THEN DATEADD(week,@Value,@Date)
	             WHEN 'quarter' THEN DATEADD(quarter,@Value,@Date)	             
	             WHEN 'hour' THEN DATEADD(Hour,@Value,@Date)	 
	             WHEN 'minute' THEN DATEADD(minute,@Value,@Date)	 
	             WHEN 'second' THEN DATEADD(second,@Value,@Date)	 
	             WHEN 'millisecond' THEN DATEADD(second,@Value,@Date)		             	             	                     
	             ELSE @Date
	       END      
	END
GO	