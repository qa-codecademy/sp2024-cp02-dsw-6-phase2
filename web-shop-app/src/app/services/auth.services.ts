// user.service.ts
import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, of, tap } from 'rxjs';
import { UserDto, UpdateUserDto, RegisterUserDto, LoginUserDto, LoginResponse } from '../types/interfaces/auth.interface';
import { apiUrl, snackBarConfig } from '../constants/app.constants';
import { CartService } from './cart.services';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { jwtDecode, JwtPayload } from 'jwt-decode'; 

@Injectable({
  providedIn: 'root'
})
export class UserService {
  isLoggedIn = signal<boolean>(false);
  private tokenKey = 'authToken'; //

  saveToken(token: string): void {
    const tokenKey = 'token';
    const existingToken = localStorage.getItem(tokenKey);
  
    if (!existingToken || existingToken !== token) {
      localStorage.setItem(tokenKey, token);
    }
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  constructor(private http: HttpClient,
    private cartServices: CartService,
    private snackBar: MatSnackBar,
    private router: Router,
    
  ) {  this.isLoggedIn.set(!!localStorage.getItem('token'))}

  // Get all users
  getUsers(): Observable<UserDto[]> {
    return this.http.get<UserDto[]>(`${apiUrl}/User/getall`);
  }

  

  getUserId(): number | null {
    const token = this.getToken();
    if (token) {
      const decodedToken: any = jwtDecode(token);
      return decodedToken?.userId || null;
    }
    return null;
  }

  getUser(): Observable<UserDto> {
    const userId = this.getUserId();  // Assuming you get the userId from somewhere
    return this.http.get<UserDto>(`${apiUrl}/User/${userId}`);
  }

  

  // Register a new user
  register(registerUserDto: RegisterUserDto): Observable<any> {
    return this.http.post(`${apiUrl}/User/register` , registerUserDto)
    .pipe(
      tap(() => {
        this.snackBar.open(
          'You have succsesfuly registerd',
          'Close',
          snackBarConfig
        )
        this.router.navigate(['/login']);
      }),
      catchError((error)=> {
        this.snackBar.open(error?.error?.errors?.[0] || `error while registering`,
          'Close',
          snackBarConfig
        );
        return of();
      })
    )
  }

  // Update a user
  updateUser(updateUserDto: UpdateUserDto): Observable<void> {
    return this.http.put<void>(`${apiUrl}/${updateUserDto.id}`, updateUserDto);
  }

  // Delete a user
  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${apiUrl}/${id}`);
  }

  // User login
  login(loginUserDto: LoginUserDto): Observable<any> {
    return this.http.post<LoginResponse>(`${apiUrl}/User/login`, loginUserDto).pipe(
      tap((response: LoginResponse) => {
        this.#setToken(response.token , response.validTo)
        if(!this.#isTokenValid()){ 
           throw new Error('Error while logigin in');
          }
          this.isLoggedIn.set(true);
          this.snackBar.open(
            'You have succsesfuly logged in',
            'Close',
            snackBarConfig
          )
  
          this.router.navigate(['/'])
  
      }),
      catchError((error)=> {
        console.log(error)
        this.snackBar.open(error?.error?.errors?.[0] || `error while logging in `,
          'Close',
          snackBarConfig
        );
        return of();
      })
    )
  }

  logout(){
    this.isLoggedIn.set(false);
    localStorage.removeItem('token');
  localStorage.removeItem('tokenExpirationDate');
  this.router.navigate(['/login']);
  }















  #isTokenValid(): boolean{
    const tokenExpirationDate: string | null = localStorage.getItem('tokenExpirationDate');
    if(!tokenExpirationDate){
      return false;
    }
    return new Date(tokenExpirationDate) > new Date();
  }
  
  
  
  #setToken(token: string , tokenExpirationDate: string) : void{
    localStorage.setItem('token', token)
    localStorage.setItem('tokenExpirationDate', tokenExpirationDate)
  }


}



