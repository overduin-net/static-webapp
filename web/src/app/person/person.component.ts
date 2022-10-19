import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService, PersonDTO } from 'src/services/generated/api.service';
import { BehaviorSubject, Subscription } from 'rxjs';


@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.scss']
})

export class PersonComponent implements OnInit {
  message = '';
  displayedColumns: string[] = ['position', 'name', 'email', 'roles'];
  datasource : PersonDTO[] | any;
  public inputValue = '';
  public persons: PersonDTO[] | undefined;

  constructor(
    private http: HttpClient,
    private apiService: ApiService
  ) { }

  ngOnInit() {
    this.inputValue = '';
    this.loadPersons();
  }

  getProfile() {
    if (this.inputValue && this.inputValue != '') {

      let person = new PersonDTO();
      person.email = this.inputValue;
      person.partitionKey = this.inputValue;
      person.name = this.inputValue;
      let s = this.apiService.createPerson(person).subscribe(x => {
        console.log("gelukt:", x)
      });
    } else {
      alert('no input value')
    }
  }

  changeInputValue(event: any) {
    this.inputValue = event.target.value;
    console.log("input value changed: ", this.inputValue);
  }

  loadPersons(){
    this.apiService.getAllPersons().subscribe(
      res => {
        if (res) {
          this.persons = res;
        }
      }
    );
  }
}