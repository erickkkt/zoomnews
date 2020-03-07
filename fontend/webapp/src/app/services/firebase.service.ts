import { Injectable } from '@angular/core';
import { AngularFirestore } from '@angular/fire/firestore';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class FirebaseService {

  constructor(private db: AngularFirestore) { }

  createUser(user: User){
    return this.db.collection('users').add(user);
  }

  getUsers(){
    return this.db.collection('users').snapshotChanges();
    
  }
}
