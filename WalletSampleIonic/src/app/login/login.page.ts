import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { Profile } from 'selenium-webdriver/firefox';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  constructor(public navCtrl : NavController) { 

   

  }
  profile(){
    this.navCtrl.navigateForward('profile');
      
  }

  ngOnInit() {
  }

}
