import { Component, Input, NO_ERRORS_SCHEMA } from "@angular/core";
import { ComponentFixture, TestBed } from "@angular/core/testing"
import { By } from "@angular/platform-browser";
import { of } from "rxjs";
import { Hero } from "../hero";
import { HeroService } from "../hero.service";
import { HeroComponent } from "../hero/hero.component";
import { HeroesComponent } from "./heroes.component"

describe('HeroesComponent (shallow) tests', () => {
    let fixture: ComponentFixture<HeroesComponent>;
    let mockHeroService;
    let HEROES;

    beforeEach(() => {

        HEROES = [
            { id: 1, name: 'SpiderDude', strenght: 3 },
            { id: 2, name: 'MonkeyDude', strenght: 5 },
            { id: 3, name: 'SuperDude', strenght: 12 }
        ]

        mockHeroService = jasmine.createSpyObj(["getHeroes", "addHero", "deleteHero"]);

        TestBed.configureTestingModule({
            declarations: [
                HeroesComponent,
                HeroComponent
            ],
            providers: [
                { provide: HeroService, useValue: mockHeroService }
            ],
            schemas: [NO_ERRORS_SCHEMA]
        });

        fixture = TestBed.createComponent(HeroesComponent);
    })

    it('should render each heroes as HeroComponent', () => {
        mockHeroService.getHeroes.and.returnValue(of(HEROES));
        fixture.detectChanges();

        const heroComponentDebugElements = 
            fixture.debugElement.queryAll(By.directive(HeroComponent));

        expect(heroComponentDebugElements.length).toBe(3);
        for (let index = 0; index < HEROES.length; index++) {
            expect(heroComponentDebugElements[index].componentInstance.hero).toEqual(HEROES[index]);
        }
    })
})