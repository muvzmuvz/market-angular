import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';




@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
  ],
  templateUrl: './app.html',
  styleUrl: './app.less',
})
export class App {
}
