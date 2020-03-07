import { Component, OnInit } from '@angular/core';
import { FirebaseService } from '../services/firebase.service';

@Component({
  selector: 'app-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.scss']
})
export class NewUserComponent implements OnInit {

  constructor(private firebaseService: FirebaseService) { }

  ngOnInit(): void {
  }

  onSubmit(value){
    this.firebaseService.createUser(value)
    .then(
      res => {
      }
    )
  }

}
