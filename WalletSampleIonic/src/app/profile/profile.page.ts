import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NavController, NavParams } from '@ionic/angular';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
})
export class ProfilePage implements OnInit {
  private username: string;
  private coins: string[];

  resule: string;
  constructor(public navCtrl: NavController, public http: HttpClient, public actroute : ActivatedRoute) {
    
    console.log(this.actroute.snapshot.NavParams.username)
    
    this.http.get('http:localhost:5000/api/Hack/' + this.username)
      .subscribe((it: any) => {
        this.resule = it
        console.log(this.resule);
      });

  }

  goBuyingPage() {
    var data = {
      username: this.username
    };
    this.http.post('http://localhost:5000/api/Hack/login', data)
      .subscribe((it: any) => {
        if (it.isSuccess) {
          this.navCtrl.navigateForward('buy');
        }
      });
  }

  goLogin() {
    this.navCtrl.navigateBack("login");
  }
  ngOnInit() {
  }

}
.