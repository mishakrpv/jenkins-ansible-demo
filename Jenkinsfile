pipeline {
  agent any

  environment {
  }

  stages {
    stage('Checkout') {
        steps {
            git 'https://github.com/mishakrpv/jenkins-ansible-demo.git'
        }
    }

    stage('Run Docker Compose') {
      steps {
        sh 'docker-compose up -d' 
      }
    }

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

    stage\('Cleanup'\) {
      steps {
        sh 'docker-compose down' 
      }
    }

    stage('Deployment') {
      steps {
        docker.stop('redis:latest').remove()
      }
    }
  }
}