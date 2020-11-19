import { Injectable, Logger } from '@nestjs/common';
import { InjectQueue } from "@nestjs/bull";
import { Queue } from "bull";

@Injectable()
export class AppService {
  constructor(@InjectQueue('calc') private audioQueue: Queue, private readonly logger: Logger) { }

  async Push(sum: number[]): Promise<any|null> {
    // delay for 5 seconds
    const job = await this.audioQueue.add({ sum: sum}, {attempts: 3, delay: 5000})
    this.logger.log(`Added Job ${job.id} to queue`,'AppService.Push')
    return job
  }
}
