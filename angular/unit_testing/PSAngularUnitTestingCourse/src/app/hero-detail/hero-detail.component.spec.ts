import { ComponentFixture, TestBed } from "@angular/core/testing";
import { ActivatedRoute } from "@angular/router";
import { HeroService } from "../hero.service";
import { HeroDetailComponent } from "./hero-detail.component";
import { Location } from '@angular/common';
import { of } from "rxjs";
import { By } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";

describe('HeroDetails unit tests', () => {

    let fixture: ComponentFixture<HeroDetailComponent>;
    let mockActivatedRoute, mockHeroService, mockLocation;

    beforeEach(() => {
        mockActivatedRoute = {  
            snapshot: { 
                paramMap: { 
                    get: () => { return '3'; } 
                } 
            } 
        };
        mockHeroService = jasmine.createSpyObj(["getHero", "updateHero"]);
        mockLocation = jasmine.createSpyObj(["back"])

        TestBed.configureTestingModule({
            imports: [FormsModule],
            declarations: [HeroDetailComponent],
            providers: [
                { provide: ActivatedRoute, useValue: mockActivatedRoute  },
                { provide: HeroService, useValue: mockHeroService  },
                { provide: Location, useValue: mockLocation  },
            ]
        });

        fixture = TestBed.createComponent(HeroDetailComponent);
        mockHeroService.getHero.and.returnValue(of({ id: 1, name: 'Mr. DogMan', strenght: 3 }));
    });

    it('should render hero name in a h2 tag', () => {
        fixture.detectChanges();
        
        let h2NativeElement = fixture.debugElement.query(By.css('h2')).nativeElement;

        expect(h2NativeElement.textContent).toContain('MR. DOGMAN');

    });

});