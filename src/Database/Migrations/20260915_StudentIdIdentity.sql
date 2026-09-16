/*
    Run once against the LMS_Database.

    SQL Server cannot add IDENTITY to an existing column, so this migration
    recreates dbo.Student while preserving its current rows and IDs.
*/

SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    -- Enrollment currently references Student.StudentId.
    ALTER TABLE dbo.Enrollment
        DROP CONSTRAINT FK_Enrollment_Student;

    -- Create the new Student table with a DB-generated identity.
    CREATE TABLE dbo.Student_Identity
    (
        StudentId       INT IDENTITY(1,1) NOT NULL,
        FirstName       NVARCHAR(100) NOT NULL,
        LastName        NVARCHAR(100) NOT NULL,
        Email           VARCHAR(255) NOT NULL,
        DepartmentId    TINYINT NOT NULL,
        StatusId        TINYINT NOT NULL,

        CONSTRAINT PK_Student_Identity
            PRIMARY KEY CLUSTERED (StudentId),

        CONSTRAINT FK_Student_Identity_StudentStatus
            FOREIGN KEY (StatusId)
            REFERENCES dbo.StudentStatus (StatusId),

        CONSTRAINT FK_Student_Identity_Department
            FOREIGN KEY (DepartmentId)
            REFERENCES dbo.Department (DepartmentId)
    );

    -- Preserve existing StudentIds.
    SET IDENTITY_INSERT dbo.Student_Identity ON;

    INSERT INTO dbo.Student_Identity
        (StudentId, FirstName, LastName, Email, DepartmentId, StatusId)
    SELECT
        StudentId, FirstName, LastName, Email, DepartmentId, StatusId
    FROM dbo.Student;

    SET IDENTITY_INSERT dbo.Student_Identity OFF;

    -- Replace the old table.
    DROP TABLE dbo.Student;

    EXEC sys.sp_rename
        N'dbo.Student_Identity',
        N'Student';

    -- Restore the original constraint names.
    EXEC sys.sp_rename
        N'dbo.PK_Student_Identity',
        N'PK_Student',
        N'OBJECT';

    EXEC sys.sp_rename
        N'dbo.FK_Student_Identity_StudentStatus',
        N'FK_Student_StudentStatus',
        N'OBJECT';

    EXEC sys.sp_rename
        N'dbo.FK_Student_Identity_Department',
        N'FK_Student_Department',
        N'OBJECT';

    -- Restore Enrollment → Student relationship.
    ALTER TABLE dbo.Enrollment WITH CHECK
        ADD CONSTRAINT FK_Enrollment_Student
        FOREIGN KEY (StudentId)
        REFERENCES dbo.Student (StudentId);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO