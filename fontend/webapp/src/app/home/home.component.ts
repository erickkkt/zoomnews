import { Component, OnInit } from '@angular/core';
import { FirebaseService } from '../services/firebase.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  items: Array<any>;
  
  constructor(public firebaseService: FirebaseService,
    private router: Router) { }


  ngOnInit(): void {
    this.firebaseService.getUsers()
      .subscribe(result => {
        this.items = result;
      })
  }

}
