using System;
using System.Collections.Generic;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Configuration;

namespace Export.API.Services
{
    public class JobDispatcherService
    {

        public JobDispatcherService()
        {
            
        }

        public void CreateJob(Guid sessionId, string mailAddress)
        {
            var config = KubernetesClientConfiguration.InClusterConfig();
            var cluster = new Kubernetes(config);
            var jobYaml = @"
apiVersion: batch/v1
kind: Job
metadata:
    name: sessions-auditlog-cleaner
    labels:
        app: sessions
    spec:
        jobTemplate:
            spec:
                template:
                    spec:
                        restartPolicy: Never
                        containers:
                            - name: sessions-auditlog-cleaner
                              image: thhdemo.azurecr.io/sessions-pdf-exporter:1.0.0
                              env:
                                - name: SQL_AZ_HOST
                                  valueFrom:
                                    secretKeyRef:
                                        name: backendsecret
                                        key: sqlHost
                                - name: SQL_AZ_USER
                                  valueFrom:
                                    secretKeyRef:
                                        name: backendsecret
                                        key: sqlUser
                                - name: AZ_SQL_PWD
                                  valueFrom:
                                    secretKeyRef:
                                        name: backendsecret
                                        key: sqlPwd
                                - name: MAILGUN_DOMAIN
                                  valueFrom:
                                    secretKeyRef:
                                        name: backendsecret
                                        key: mailGunDomain
                                - name: MAILGUN_API_KEY
                                  valueFrom:
                                    secretKeyRef:
                                        name: backendsecret
                                        key: mailGunApiKey
                                - name: AZ_SESSION_STORAGE
                                  valueFrom:
                                    secretKeyRef:
                                        name: backendsecret
                                        key: azStorageAccount
                                - name: AZ_SESSION_STORAGE_KEY 
                                  valueFrom:
                                    secretKeyRef:
                                        name: backendsecret
                                        key: azStorageAcccountKey
                                - name: SESSION_ID 
                                  value: """ + sessionId.ToString() +  @"""
                                - name: RECIPIENT_MAIL_ADDR 
                                  value: """ + mailAddress + @"""
             ";

            var job = Yaml.LoadFromString<V1Job>(jobYaml);
            cluster.CreateNamespacedJob(job, "");
        }
    }
}
