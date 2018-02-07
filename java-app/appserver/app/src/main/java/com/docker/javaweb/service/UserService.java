package com.docker.javaweb.service;

import com.docker.javaweb.model.User;

public interface UserService {
	User save(User user);
	boolean findByLogin(String userName, String password);
	boolean findByUserName(String userName);
}
