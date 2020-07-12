import { Component, OnInit } from '@angular/core';
import {StudentService} from '../shared/student.service';
import {Student} from '../shared/student.model'

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  studentList:any;
  enableEdit:boolean;
  enableEditIndex:any;
  enableEditIndexParent:any;
  SubjectList:any;
  constructor(private Service : StudentService) { }

  ngOnInit(): void {    
    this.Service.GetSubjectList().subscribe(data => {
      this.SubjectList = data;    
    });
    this.Service.refreshNeeded$.subscribe(()=>{
      this.GetStudent();
    })

    this.GetStudent();
     
  }
  DeleteSubject(SubjectID,StudentID){
this.Service.DeleteStudentSubject(SubjectID,StudentID).subscribe()
this.GetStudent();
  }
  enableEditMethod(e, i,j) {
    this.enableEdit = true;
    this.enableEditIndex = i;
    this.enableEditIndexParent=j;
    
  }
GetStudent(){
  this.Service.GetStudentList().subscribe(data => {
    this.studentList = data;      
  });
}
saveSegment(i,j) {
  console.log(i+j); 
  } 
EditStudentList(element,SubjectID,StudentID){
     alert(SubjectID +"-"+ StudentID+"--"+ element);
  }
}
