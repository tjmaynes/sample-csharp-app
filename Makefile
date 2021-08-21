ENVIRONMENT := development

include .env.$(ENVIRONMENT)
export $(shell sed 's/=.*//' .env.$(ENVIRONMENT))

install_dependencies:
	./scripts/$@.sh

test:
	./scripts/$@.sh

build:
	dotnet $@

start:
	dotnet run --project src/ShoppingService.Api

build_artifact:
	dotnet publish -c Release -o dist

archive_artifact: build_artifact
	zip -r ShoppingCartAPI.zip dist

build_image:
	./scripts/$@.sh

push_image:
	./scripts/$@.sh

deploy_image: build_image push_image

deploy: archive_artifact deploy_image

ship_it: build test deploy

create_db_init_script:
	./scripts/$@.sh
