/*
    Run once against the LMS database.
    SQL Server cannot add IDENTITY to an existing column, so this migration
    recreates dbo.Enrollment while preserving its current rows and IDs.
*/
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    CREATE TABLE dbo.Enrollment_Identity
    (
        EnrollmentId      INT IDENTITY(1,1) NOT NULL,
        StudentId         INT NOT NULL,
        CourseOfferingId  INT NOT NULL,
        StatusId          TINYINT NOT NULL,
        EnrollmentDate    DATETIME2(7) NOT NULL,
        Price             DECIMAL(18,2) NOT NULL,
        DiscountTypeId    TINYINT NULL,
        DiscountValue     DECIMAL(18,2) NOT NULL,
        DiscountCode      VARCHAR(50) NULL,
        CONSTRAINT PK_Enrollment_Identity PRIMARY KEY CLUSTERED (EnrollmentId)
    );

    SET IDENTITY_INSERT dbo.Enrollment_Identity ON;

    INSERT INTO dbo.Enrollment_Identity
        (EnrollmentId, StudentId, CourseOfferingId, StatusId, EnrollmentDate,
         Price, DiscountTypeId, DiscountValue, DiscountCode)
    SELECT
        EnrollmentId, StudentId, CourseOfferingId, StatusId, EnrollmentDate,
        Price, DiscountTypeId, DiscountValue, DiscountCode
    FROM dbo.Enrollment;

    SET IDENTITY_INSERT dbo.Enrollment_Identity OFF;

    DROP TABLE dbo.Enrollment;
    EXEC sys.sp_rename N'dbo.Enrollment_Identity', N'Enrollment';

    ALTER TABLE dbo.Enrollment WITH CHECK ADD CONSTRAINT FK_Enrollment_CourseOffering
        FOREIGN KEY (CourseOfferingId) REFERENCES dbo.CourseOffering (CourseOfferingId);
    ALTER TABLE dbo.Enrollment WITH CHECK ADD CONSTRAINT FK_Enrollment_DiscountType
        FOREIGN KEY (DiscountTypeId) REFERENCES dbo.DiscountType (DiscountTypeId);
    ALTER TABLE dbo.Enrollment WITH CHECK ADD CONSTRAINT FK_Enrollment_Status
        FOREIGN KEY (StatusId) REFERENCES dbo.EnrollmentStatus (StatusId);
    ALTER TABLE dbo.Enrollment WITH CHECK ADD CONSTRAINT FK_Enrollment_Student
        FOREIGN KEY (StudentId) REFERENCES dbo.Student (StudentId);

    CREATE UNIQUE NONCLUSTERED INDEX UX_Enrollment_Active_Student_CourseOffering
        ON dbo.Enrollment (StudentId, CourseOfferingId)
        WHERE StatusId IN (1, 2);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO
