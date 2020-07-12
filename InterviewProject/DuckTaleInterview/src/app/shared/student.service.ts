import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http/';
import{Student} from '../shared/student.model';
import { delay, tap } from "rxjs/operators"
import { Subject } from 'rxjs';

let headers = new HttpHeaders({
  'Content-Type': 'application/json'});
let options = { headers: headers };

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  studentList:Student[]=[];
  FormData:Student;
  
  private _refreshNeeded$= new Subject<void>();

  get refreshNeeded$(){
    return this._refreshNeeded$;
  }
  private readonly BaseURL="http://localhost:56185/"
  constructor(private http:HttpClient) { }
  GetStudentList(){
   //this.studentList.map(val=>this.http.get(this.BaseURL+"api/GetDetails/GetList"));
   return this.http.get(this.BaseURL+"/api/GetDetails/GetList");
  }
  GetSubjectList(){
    return this.http.get(this.BaseURL+"/api/GetDetails/GetAllSubjects");
  }
  InserStudent(formData){
    return this.http.post(this.BaseURL+"/api/GetDetails/InsertStudent",JSON.stringify(formData), options)
             .pipe(
               tap(() => {
                 this._refreshNeeded$.next();
               })
             );
  }
  DeleteStudentSubject(SubjectID,StudentID){
    return this.http.delete(this.BaseURL+"/api/GetDetails/DeleteStudentSubject?StudentID="+StudentID+"&SubjectID="+SubjectID+"")
  }
}
