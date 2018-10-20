import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { LoginPage } from '../login/login.page';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.page.html',
  styleUrls: ['./buy.page.scss'],
})
export class BuyPage implements OnInit {

  private coins: any[] = [];
  private selectedCoin: any = {};
  private buyAmount: number = 0;

  constructor(public navCtrl: NavController, private http: HttpClient) {

  }

  ngOnInit() {
    this.http.get('http://hackathoncoins.azurewebsites.net/api/Hack/coinprices')
      .subscribe((it: any[]) => {
        this.coins = it;
      });
  }

  buy() {
    var data = {
      username: LoginPage.CurrentUsername,
      symbol: this.selectedCoin.symbol,
      usdValue: this.buyAmount
    };
    this.http.post('http://hackathoncoins.azurewebsites.net/api/Hack/buy', data)
      .subscribe((it:any) => {
         if(it.isSuccess){
           this.navCtrl.navigateBack("profile");
         }
         else{
           console.log(it)
         }
      });
  }

}