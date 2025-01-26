import {LiveAnnouncer} from '@angular/cdk/a11y';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService, PersonDTO } from 'src/services/generated/api.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';


@Component({
    selector: 'app-person',
    templateUrl: './person.component.html',
    styleUrls: ['./person.component.scss'],
    standalone: false
})

export class PersonComponent implements OnInit {
  message = '';
  displayedColumns: string[] = ['id', 'email', 'name'];
  //datasource: PersonDTO[] | any;
  datasource: MatTableDataSource<PersonDTO>;
  public inputValue = '';
  public persons: PersonDTO[] | undefined;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private _liveAnnouncer: LiveAnnouncer,
    private apiService: ApiService
  ) { 
    this.inputValue = '';
    this.loadPersons();
   }

  ngOnInit() {
    // this.inputValue = '';
    // this.loadPersons();
  }

  ngAfterViewInit() {
    
  }

  getProfile() {
    if (this.inputValue && this.inputValue != '') {

      let person = new PersonDTO();
      person.name = this.inputValue;
      let s = this.apiService.createPerson(person).subscribe(x => {
        console.log("gelukt:", x)
      });
    } else {
      alert('no input value')
    }
  }

  public doFilter = (value: string) => {
    this.datasource.filter = value.trim().toLocaleLowerCase();
  }

  changeInputValue(event: any) {
    this.inputValue = event.target.value;
    console.log("input value changed: ", this.inputValue);
  }

  loadPersons() {
    this.apiService.getAllPersons().subscribe(
      res => {
        if (res) {
          this.datasource = new MatTableDataSource<PersonDTO>(res);
          this.datasource.paginator = this.paginator;
          this.datasource.sort = this.sort;
        }
      }
    );
  }
}