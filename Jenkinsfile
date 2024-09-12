pipeline {
  agent any

  environment {
  }

  stages {
    stage('Build') {
      steps {
        docker.build(imageName: 'redis:latest', dockerfile: './Dockerfile').push()
        docker.run(image: 'redis:latest', detached: true, portMappings: [containerPort: 6379, hostPort: 6379])
      }
    }

    stage('Testing') {
      steps {
        sh './build.sh'
      }
    }

    stage('Deployment') {
      steps {
        docker.stop('redis:latest').remove()
      }
    }
  }
}