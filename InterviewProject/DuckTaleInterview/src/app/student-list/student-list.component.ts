import { Component, OnInit } from '@angular/core';
import { Student,Mark } from '../shared/student.model';
import { StudentService } from '../shared/student.service';
import { NgForm, FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {
  SubjectList:any;
  mar:Mark;
  studentForm : FormGroup;
  constructor(public Service:StudentService, private fb: FormBuilder) { }

  get marks(): FormArray {
    return this.studentForm.get('marks') as FormArray;
  }

  ngOnInit(): void {
    // this.resetForm();
    this.Service.GetSubjectList().subscribe(data => {
      this.SubjectList = data;    
    });

    this.studentForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      marks: this.fb.array([this.buildMarkArr()])
    })

  }

  addMore(): void {
    this.marks.push(this.buildMarkArr());
  }

  buildMarkArr() : FormGroup {
    return this.fb.group({
      SubjectID: '',
      Marks: ''
    });
  }

  save() {
    console.log(this.studentForm);
    console.log('Saved: ' + JSON.stringify(this.studentForm.value));
    this.Service.InserStudent(this.studentForm.value).subscribe(data =>{
      this.studentForm.reset();      
      console.log(data)});
    } 
  

}
