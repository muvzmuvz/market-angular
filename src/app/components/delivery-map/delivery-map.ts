import { FormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { TuiRadio } from '@taiga-ui/kit';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TuiCard } from '@taiga-ui/layout';
// Remove LeafletModule from here and import it in your module file instead
@Component({
  selector: 'app-delivery-map',
  templateUrl: './delivery-map.html',
  styleUrl: './delivery-map.scss',
  imports: [CommonModule, TuiRadio, FormsModule, TuiCard]
})
export class DeliveryMap {
  deliveryOptions = ['Пункт выдачи', 'Курьером'];
  selectedOption = 'Пункт выдачи';

  mapUrl: SafeResourceUrl;
option: any;

  constructor(private sanitizer: DomSanitizer) {
    const lat = 52.3702;
    const lon = 4.8952;

    const url = `https://www.openstreetmap.org/export/embed.html?bbox=${lon - 0.01}%2C${lat - 0.01}%2C${lon + 0.01}%2C${lat + 0.01}&layer=mapnik&marker=${lat}%2C${lon}`;
    this.mapUrl = this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
}
