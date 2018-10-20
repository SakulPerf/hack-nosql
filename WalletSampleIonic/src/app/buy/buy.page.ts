import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.page.html',
  styleUrls: ['./buy.page.scss'],
})
export class BuyPage implements OnInit {

  constructor(public navCtrl: NavController, private http: HttpClient) { 

  }

  ngOnInit() {
  }

  profile(){
    this.navCtrl.navigateBack("profile");
  }

}