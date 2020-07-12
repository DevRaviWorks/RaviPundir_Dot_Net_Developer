
    export interface Mark {
        SubjectID: number;
        SubjectName: string;
        Marks: number;
    }

    export interface Student {
        StudentID: number;
        FirstName: string;
        LastName: string;
        Marks: Mark[];
    }

