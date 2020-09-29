import { StrengthPipe } from './strength.pipe';

let pipe: StrengthPipe;

beforeEach(() => {
    pipe = new StrengthPipe();
})

describe('StrengthPipe', () => {
    it('should display weak if strength is 5', () => {
        expect(pipe.transform(5)).toEqual('5 (weak)');
    })

    it('should display weak if strength is 10', () => {
        expect(pipe.transform(10)).toEqual('10 (strong)');
    })
})