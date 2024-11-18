import { Component } from '@angular/core';
import { UserService } from '../../services/auth.services';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { EmailValidator, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input'
import { LoginUserDto, RegisterUserDto } from '../../types/interfaces/auth.interface';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  hidePassword: boolean = true;

  registerForm: FormGroup = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    lastname: new FormControl<string>('', Validators.required),
    username: new FormControl<string>('', Validators.required),
    address: new FormControl<string>('', Validators.required),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    phone: new FormControl<string>('', Validators.required),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(7)]),
    confirmPassword: new FormControl<string>('', [Validators.required, Validators.minLength(7)])
  })
 
  get hasUNameRequiredError(): boolean {
    return !!(this.registerForm.get('username')?.hasError('required') &&
      (this.registerForm.get('username')?.touched || this.registerForm.get('username')?.dirty)
    )
  }
  get hasNameRequiredError(): boolean {
    return !!(this.registerForm.get('name')?.hasError('required') &&
      (this.registerForm.get('name')?.touched || this.registerForm.get('name')?.dirty)
    )
  }

  get hasLNameRequiredError(): boolean {
    return !!(this.registerForm.get('lastname')?.hasError('required') &&
      (this.registerForm.get('lastname')?.touched || this.registerForm.get('lastname')?.dirty)
    )
  }


  get hasAddressRequiredErro(): boolean {
    return !!(this.registerForm.get('address')?.hasError('required') &&
      (this.registerForm.get('address')?.touched || this.registerForm.get('address')?.dirty)
    )
  }

get hasEmailRequiredErro(): boolean {
    return !!(this.registerForm.get('email')?.hasError('required') &&
      (this.registerForm.get('email')?.touched || this.registerForm.get('email')?.dirty)
    )
  }

  get hasPhoneRequiredErro(): boolean {
    return !!(this.registerForm.get('phone')?.hasError('required') &&
      (this.registerForm.get('phone')?.touched || this.registerForm.get('phone')?.dirty)
    )
  }
  


  get hasPasswordRequiredError(): boolean {
    return !!(this.registerForm.get('password')?.hasError('required') &&
      (this.registerForm.get('password')?.touched || this.registerForm.get('password')?.dirty)
    )
  }
  get hasCPasswordRequiredError(): boolean {
    return !!(this.registerForm.get('confirmPassword')?.hasError('required') &&
      (this.registerForm.get('confirmPassword')?.touched || this.registerForm.get('confirmPassword')?.dirty)
    )
  }

  constructor(
    private authService: UserService
  ) {}

  onRegister() {
    this.authService.register(this.registerForm.value as RegisterUserDto)
    .subscribe(); 
  }



}