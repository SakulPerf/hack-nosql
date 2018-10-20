import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { Profile, createWiresService } from 'selenium-webdriver/firefox';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  private username: string;

  constructor(public navCtrl: NavController, private http: HttpClient) {
  }

  login() {
    var data = {
      username: this.username
    };
    this.http.post('http://localhost:58514/api/Hack/login', data)
      .subscribe((it:any) => {
          if(it.isSuccess){
            this.navCtrl.navigateForward('profile');
          }
      });
  }

  ngOnInit() {
  }

}
