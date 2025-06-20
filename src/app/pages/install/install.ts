import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SiteInitService, SiteInitData } from 'src/app/service/site-init-service';
import { FormsModule } from '@angular/forms';
import { TuiButton } from '@taiga-ui/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { HomePage } from '../home-page/home-page';
@Component({
  selector: 'app-install',
  imports: [FormsModule, TuiButton, HttpClientModule, CommonModule],
  templateUrl: './install.html',
  styleUrl: './install.less',
  providers: [SiteInitService],
  standalone: true
})
export class Install {
  form: SiteInitData = {
    siteName: '',
    name: '',
    password: '',
    email: ''
  };

  error: string | null = null;

  constructor(
    private siteInitService: SiteInitService,
    private router: Router
  ) { }

  install() {
    this.siteInitService.initSite(this.form).subscribe({
      next: () => {
        this.router.navigate([HomePage]); // переход после успешной установки
      },
      error: (err) => {
        this.error = err.error?.message || 'Произошла ошибка';
      }
    });
  }
}