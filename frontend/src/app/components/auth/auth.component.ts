import { Component, OnInit } from '@angular/core';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';
import { HttpClientModule } from '@angular/common/http';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from "../register/register.component";
import { LoginComponent } from '../login/login.component';

@Component({
    selector: 'app-login',
    standalone: true,
    templateUrl: './auth.component.html',
    styleUrl: './auth.component.css',
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        LoadingIndicatorComponent,
        MatButtonToggleModule,
        LoginComponent,
        RegisterComponent
    ]
})

export class AuthComponent  implements OnInit {
  selectedOption: string = 'login';
  isLoadingForm: boolean = false;

  ngOnInit(): void {
    this.isLoadingForm = true;
  }
}
