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

    stage('Build') {
      steps {
        echo 'Building..'

        docker.build(imageName: 'redis:latest', dockerfile: './Dockerfile').push()
        docker.run(image: 'redis:latest', detached: true, portMappings: [containerPort: 6379, hostPort: 6379])

        sh 'dotnet restore'
        sh 'dotnet build --no-restore'
      }
    }

    stage('Test') {
      steps {
        echo 'Testing..'

        sh 'dotnet test --no-build --verbosity normal'
      }
    }

    stage('Deploy') {
      steps {
        echo 'Deploying..'

        docker.stop('redis:latest').remove()
      }
    }
  }
}