package com.docker.javaweb.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.client.RestTemplate;
import org.springframework.http.converter.StringHttpMessageConverter;
import org.springframework.http.converter.json.MappingJacksonHttpMessageConverter;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpEntity;
import org.springframework.http.ResponseEntity;
import org.springframework.http.HttpStatus;
import com.docker.javaweb.model.User;
import com.docker.javaweb.repository.UserRepository;

import java.util.Random;

@Service("userService")
public class UserServiceImpl implements UserService {

	String baseUri = new String("http://dotnet-api/api/users");
	RestTemplate rt = new RestTemplate();	

	public User save(User user) {
		System.out.println(user.toString());
		String uri = baseUri;
		rt.getMessageConverters().add(new MappingJacksonHttpMessageConverter());
		HttpHeaders headers = new HttpHeaders();
        headers.add("Content-Type", "application/json");
		HttpEntity<User> entity = new HttpEntity<User>(user, headers);	
		User usr = rt.postForObject(uri, entity, User.class);
		return usr;
	}

	public boolean findByLogin(String userName, String password) {	
		User user = findByUserName(userName);
		if(user != null && user.getPassword().equals(password)) {
			return true;
		} 
		return false;		
	}

	public boolean userExists(String userName) {
		String uri = baseUri + "/" + userName;
		try {
			ResponseEntity<String> response = rt.getForEntity(uri, String.class);
			HttpStatus status = response.getStatusCode();
			if(status.name().compareTo("OK") == 0)	{
				return true;
			}
		} catch(Exception e) {
			System.out.println(e);
		}
		return false;
	}

	public User findByUserName(String userName) {
		String uri = baseUri + "/" + userName;
		User user = rt.getForObject(uri, User.class);
		return user;
	}

}
