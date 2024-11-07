import { Component } from '@angular/core';
import { UserService } from '../../services/auth.services';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input'
import { LoginUserDto } from '../../types/interfaces/auth.interface';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  hidePassword: boolean = true;

  loginForm: FormGroup = new FormGroup({
    username: new FormControl<string>('', Validators.required),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(7)])
  })
 
  get hasNameRequiredError(): boolean {
    return !!(this.loginForm.get('username')?.hasError('required') &&
      (this.loginForm.get('username')?.touched || this.loginForm.get('username')?.dirty)
    )
  }

  get hasPasswordRequiredError(): boolean {
    return !!(this.loginForm.get('password')?.hasError('required') &&
      (this.loginForm.get('password')?.touched || this.loginForm.get('password')?.dirty)
    )
  }

  constructor(
    private authService: UserService
  ) {}

  onLogin() {
    this.authService.login(this.loginForm.value as LoginUserDto)
    .subscribe(); 
  }



}