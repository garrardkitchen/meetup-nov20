import {
    Processor,
    Process,
    OnQueueActive,
    OnGlobalQueueCompleted,
    OnQueueProgress,
    OnQueueCompleted, OnQueueFailed
} from '@nestjs/bull';
import { Job } from 'bull';
import { Logger } from "@nestjs/common";

@Processor('calc')
export class Consumer {
    constructor(private readonly logger: Logger) {
    }
    @Process()
    async onProcess(job: Job<unknown>) {
        this.logger.log(`Job Attempt: ${job.attemptsMade} `, 'Consumer.onProcess')
        let progress = 0;

        // simulate exception being thrown
        if (job.attemptsMade <2) {
            throw new Error("FORCED ERROR")
        }

        const data: number[] = job.data['sum']

        for (let i = 0; i < data.length; i++) {
            progress = (i/data.length) * 100
            job.progress(progress);
        }
        const result = data.reduce((a,b) => a+b)
        return result;
    }

    @OnQueueProgress()
    onProgress(job: Job,  progress: number) {
        this.logger.log(`Progressing job ${job.id} is ${progress}...`,'Consumer.onProgress');
    }

    @OnQueueActive()
    onActive(job: Job) {
        this.logger.log(`Processing job ${job.id} with data ${job.data['sum']}...`, 'Consumer.onActive')
    }

    @OnQueueFailed()
    async onQueueFailed(job: Job, err: Error){
        this.logger.warn(`on failure: ${job.id}`, 'Consumer.onQueueFailed');
    }

    @OnQueueCompleted()
    async onQueueCompleted(job: Job) {
         this.logger.log(`on completed job: ${job.id}`, 'Consumer.onQueueCompleted');
    }
}