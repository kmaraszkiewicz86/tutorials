import { MergeScanSubscriber } from 'rxjs/internal/operators/mergeScan';
import { MessageService } from './message.service';

describe('MessageService', () => {

    let service: MessageService;

    beforeEach(() => {
        service = new MessageService()
    })

    it('should have no message to start', () => {
        expect(service.messages.length).toBe(0);
    })

    it('should add message when add i called', () => {
        service.add('Message 1');

        expect(service.messages.length).toBe(1);
    })

    it('should remove message when clear is called', () => {
        service.add('Message 1');

        service.clear();

        expect(service.messages.length).toBe(0);
    })

})