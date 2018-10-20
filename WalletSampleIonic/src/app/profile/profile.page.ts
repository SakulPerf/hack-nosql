import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NavController, NavParams } from '@ionic/angular';
import { LoginPage } from '../login/login.page';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
})
export class ProfilePage implements OnInit {

  private username: string;
  private wallet: any = {};

  constructor(public navCtrl: NavController, public http: HttpClient, public actroute: ActivatedRoute) {
  }

  goBuyingPage() {
    this.navCtrl.navigateForward('buy');
  }

  goLogin() {
    this.navCtrl.navigateBack("login");
  }

  ngOnInit() {
    this.reloadProfile();
  }

  ionViewDidLoad() {
    this.reloadProfile();
  }

  private reloadProfile(){
    this.username = LoginPage.CurrentUsername;
    this.http.get('http://hackathoncoins.azurewebsites.net/api/Hack/' + LoginPage.CurrentUsername)
      .subscribe((it: any) => {
        this.wallet = it
      });
  }

}