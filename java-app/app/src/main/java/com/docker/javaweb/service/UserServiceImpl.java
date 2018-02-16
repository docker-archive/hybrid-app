package com.docker.javaweb.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.client.RestTemplate;
import org.springframework.http.converter.StringHttpMessageConverter;
import org.springframework.http.converter.json.MappingJacksonHttpMessageConverter;

import com.docker.javaweb.model.User;
import com.docker.javaweb.repository.UserRepository;

@Service("userService")
public class UserServiceImpl implements UserService {

	String baseUri = "http://localhost:57989/api/users";
	RestTemplate rt = new RestTemplate();

	@Autowired
	private UserRepository userRepository;
	
	// @Transactional
	public User save(User user) {
		String uri = baseUri;
		User usr = rt.postForObject(uri, user, User.class);
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
		User user = rt.getForObject(uri, User.class);
		if(user != null) {
			return true;
		}
		return false;
	}

	public User findByUserName(String userName) {
		
		String uri = baseUri + "/" + userName;
		User user = rt.getForObject(uri, User.class);
		return user;
	}

}
