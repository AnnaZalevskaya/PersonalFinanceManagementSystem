import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { CarouselModule, OwlOptions } from 'ngx-owl-carousel-o';

@Component({
  selector: 'app-templates-carousel',
  standalone: true,
  imports: [
    CarouselModule,
    CommonModule
  ],
  templateUrl: './templates-carousel.component.html',
  styleUrl: './templates-carousel.component.css'
})
export class TemplatesCarouselComponent {
  carouselOptions!: OwlOptions;
  carouselItems: any;

  ngOnInit() {
    this.carouselOptions = {
      loop: true,
      mouseDrag: true,
      touchDrag: true,
      pullDrag: true,
      dots: true,
      navSpeed: 700,
      navText: ['', ''],
      responsive: {
        0: {
          items: 1
        }
      }
    };

    this.carouselItems = [   
      { imageUrl: 'assets/images/template3.jpg' },
      { imageUrl: 'assets/images/template1.png' },
      { imageUrl: 'assets/images/template2.png' },
      { imageUrl: 'assets/images/template1.jpg' },
      { imageUrl: 'assets/images/template2.jpg' }
    ];
  }
}
