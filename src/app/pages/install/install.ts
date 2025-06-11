import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { TuiButton } from '@taiga-ui/core';
@Component({
  selector: 'app-install',
  imports: [FormsModule, TuiButton],
  templateUrl: './install.html',
  styleUrl: './install.less'
})
export class Install {
form: any;
  http: any;
  router: any;
  error: any;
install() {
  this.http.post('/api/install', this.form).subscribe({
    next: () => this.router.navigate(['/login']),
    error: (error: { error: any; }) => this.error = error.error
  });
}
}
